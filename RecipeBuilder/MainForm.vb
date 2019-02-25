Imports System.ComponentModel
Imports System.IO

Public Class MainForm

    Dim currentFile As String = ""
    Dim fileChanged As Boolean = False


    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        fileChanged = False
    End Sub



    Private Sub DataGridView1_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles DataGridView1.RowsAdded
        If DataGridView1.RowCount > 2 Then 'Copies values from above to fill blanks in table
            For z = 1 To DataGridView1.ColumnCount - 1
                DataGridView1.Item(z, DataGridView1.RowCount - 2).Value = DataGridView1.Item(z, DataGridView1.RowCount - 3).Value
            Next
        End If
    End Sub

    Private Sub DataGridView1_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged

        fileChanged = True

        If e.RowIndex = -1 Then
            Exit Sub
        End If

        If Not IsNumeric(DataGridView1.Item(e.ColumnIndex, e.RowIndex).Value) Then 'Check to make sure entered values are numeric and change to 0 if not
            DataGridView1.Item(e.ColumnIndex, e.RowIndex).Value = 0
        End If

        If e.ColumnIndex = 2 Then 'Sets max value for temperature
            If DataGridView1.Item(e.ColumnIndex, e.RowIndex).Value > 500 Then
                DataGridView1.Item(e.ColumnIndex, e.RowIndex).Value = 500
                Me.ToolStripStatusLabel1.Text = "Error: Max temperature is 500 °F."
            End If
        End If

        If e.ColumnIndex = 2 Then 'Sets min value for temperature
            If DataGridView1.Item(e.ColumnIndex, e.RowIndex).Value < 70 Then
                DataGridView1.Item(e.ColumnIndex, e.RowIndex).Value = 70
                Me.ToolStripStatusLabel1.Text = "Error: Min temperature is 70 °F."
            End If
        End If

        If e.ColumnIndex = 3 Then 'Set max pressure
            If DataGridView1.Item(e.ColumnIndex, e.RowIndex).Value > 200 Then
                Me.ToolStripStatusLabel1.Text = "Error: Max pressure is 200 psi."
                DataGridView1.Item(e.ColumnIndex, e.RowIndex).Value = 200
            End If
        End If

        If e.ColumnIndex = 3 Then 'Set min pressure
            If DataGridView1.Item(e.ColumnIndex, e.RowIndex).Value < 0 Then
                Me.ToolStripStatusLabel1.Text = "Error: Min pressure is 0 psi."
                DataGridView1.Item(e.ColumnIndex, e.RowIndex).Value = 0
            End If
        End If

        If e.ColumnIndex = 1 Then 'Sets min value for time
            If DataGridView1.Item(e.ColumnIndex, e.RowIndex).Value < 0 Then
                DataGridView1.Item(e.ColumnIndex, e.RowIndex).Value = 0
                Me.ToolStripStatusLabel1.Text = "Error: Min time is 0 min."
            End If
        End If

        If DataGridView1.RowCount > 1 Then 'Enter values into the chart if there is more that 1 row

            For i = 0 To Chart1.Series.Count - 1 'Clear all of the previously entered values in the chart
                If Chart1.Series.Item(i).Points.Count > 0 Then
                    Chart1.Series.Item(i).Points.Clear()
                End If

                If i = 0 Then 'Add a start point each series of the chart
                    Chart1.Series.Item(i).Points.AddXY(0, 70)
                Else
                    Chart1.Series.Item(i).Points.AddXY(0, 0)
                End If

            Next

            Dim xVal As Integer = 0

            For i = 0 To DataGridView1.RowCount - 1 'Add values for each row in the table
                Dim yVal As Integer = 0

                yVal = DataGridView1.Item(4, i).Value
                Chart1.Series.Item(2).Points.AddXY(xVal, yVal * 50) 'Input vacuum data


                xVal = xVal + DataGridView1.Item(1, i).Value 'Add current time to xVal


                yVal = DataGridView1.Item(2, i).Value
                Chart1.Series.Item(0).Points.AddXY(xVal, yVal) 'Input temperature data

                yVal = DataGridView1.Item(3, i).Value
                Chart1.Series.Item(1).Points.AddXY(xVal, yVal) 'Input pressure data

                yVal = DataGridView1.Item(4, i).Value
                Chart1.Series.Item(2).Points.AddXY(xVal, yVal * 50) 'Input vacuum data
            Next
        End If
    End Sub

    Private Sub OpenBtn_Click(sender As Object, e As EventArgs) Handles OpenBtn.Click

        If fileChanged = True Then
            If MsgBox("Would you like to save the current edits?", vbYesNo, "Save Check") = vbYes Then
                Call SaveBtn_Click(sender, e)
            End If
        End If

        If OpenFileDialog.ShowDialog() = DialogResult.OK Then
            openFile(OpenFileDialog.FileName)
        End If

    End Sub

    Sub openFile(filePath As String)
        currentFile = filePath

        Dim fs As New IO.FileStream(filePath, IO.FileMode.Open, IO.FileAccess.ReadWrite)
        Dim reader As New StreamReader(fs)
        Dim readValues As String = reader.ReadToEnd
        fs.Close()

        Dim recipeName As String = System.IO.Path.GetFileNameWithoutExtension(filePath)
        Chart1.Titles.Item(0).Text = recipeName & " || Cure Profile"
        Me.Text = "Autoclave Recipe Builder || " & recipeName


        Dim time() As String = settingsArr(readValues, "AutoClaveDesktop:Integer Table.nTable_TempTimeSetpoints_Edit")
        Dim temp() As String = settingsArr(readValues, "AutoClaveDesktop:Float Table.fTable_OvenTemp_Setpoints_Edit")
        Dim press() As String = settingsArr(readValues, "AutoClaveDesktop:Float Table.fTable_OvenPress_Setpoints_Edit")
        Dim vac() As String = settingsArr(readValues, "AutoClaveDesktop:Integer Table.nTable_VacuumOnOff_Edit")


        Do While DataGridView1.Rows.Count > UBound(time) + 1
            DataGridView1.Rows.RemoveAt(0)
        Loop

        Do While DataGridView1.Rows.Count < UBound(time) + 1
            DataGridView1.Rows.Add(1)
        Loop


        For i = 0 To UBound(time)
            DataGridView1.Item(1, i).Value = time(i)
        Next

        For i = 0 To UBound(temp)
            DataGridView1.Item(2, i).Value = temp(i)
        Next

        For i = 0 To UBound(press)
            DataGridView1.Item(3, i).Value = press(i)
        Next

        For i = 0 To UBound(vac)
            DataGridView1.Item(4, i).Value = vac(i)
        Next

        fileChanged = False
    End Sub

    Function settingsArr(inFullTxt As String, setSearch As String) As String()
        Dim returnArr() As String

        Dim stringSection As String = Strings.Right(inFullTxt, Len(inFullTxt) - InStr(inFullTxt, setSearch) - Len(setSearch) - 1)
        stringSection = Strings.Left(stringSection, InStr(stringSection, vbCrLf & vbCrLf) - 1)

        returnArr = Split(stringSection, vbCrLf)

        For i = 0 To UBound(returnArr)
            returnArr(i) = Strings.Right(returnArr(i), Len(returnArr(i)) - InStr(returnArr(i), ":"))
        Next

        Return returnArr
    End Function

    Private Sub DataGridView1_RowStateChanged(sender As Object, e As DataGridViewRowStateChangedEventArgs) Handles DataGridView1.RowStateChanged
        If DataGridView1.RowCount > 0 Then 'Adds segment numbers to table
            For i = 0 To DataGridView1.RowCount - 1
                DataGridView1.Item(0, i).Value = i + 1
            Next
        End If

        For i = 0 To DataGridView1.RowCount - 1 'Fill all blank cells with 0
            For z = 1 To DataGridView1.ColumnCount - 2
                If Not IsNumeric(DataGridView1.Item(z, i).Value) Then
                    If z = 2 Then
                        DataGridView1.Item(z, i).Value = 70
                    Else
                        DataGridView1.Item(z, i).Value = 0
                    End If

                End If
            Next
        Next
    End Sub

    Private Sub OpenFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenFileToolStripMenuItem.Click
        OpenBtn.PerformClick()
    End Sub

    Private Sub SaveBtn_Click(sender As Object, e As EventArgs) Handles SaveBtn.Click
        If currentFile = "" Then
            Call saveAs()
            Exit Sub
        End If

        Dim recStr As String = getRecipe(currentFile)
        IO.File.WriteAllText(currentFile, recStr)
        openFile(currentFile)

    End Sub

    Sub saveAs()
        If SaveFileDialog.ShowDialog() = DialogResult.OK Then
            Dim recStr As String = getRecipe(SaveFileDialog.FileName)
            IO.File.WriteAllText(SaveFileDialog.FileName, recStr)
            openFile(SaveFileDialog.FileName)
        End If
    End Sub

    Function getRecipe(fileName As String) As String

        Dim time As New List(Of String)
        Dim temp As New List(Of String)
        Dim press As New List(Of String)
        Dim vac As New List(Of String)

        For i = 0 To DataGridView1.Rows.Count - 1
            time.Add(i & ":" & DataGridView1.Item(1, i).Value & vbCrLf)
            temp.Add(i & ":" & DataGridView1.Item(2, i).Value & vbCrLf)
            press.Add(i & ":" & DataGridView1.Item(3, i).Value & vbCrLf)

            Dim vacVal As Integer = 0
            If DataGridView1.Item(4, i).Value = Nothing Then
                vacVal = 0
            Else
                vacVal = DataGridView1.Item(4, i).Value
            End If

            vac.Add(i & ":" & vacVal & vbCrLf)
        Next

        Dim recipeString As String = "AutoClaveDesktop:Integer Table.nTable_TempTimeSetpoints_Edit" & vbCrLf & String.Join("", time.ToArray()) & vbCrLf &
            "AutoClaveDesktop:Float Table.fTable_OvenTemp_Setpoints_Edit" & vbCrLf & String.Join("", temp.ToArray()) & vbCrLf &
            "AutoClaveDesktop:Float Table.fTable_OvenPress_Setpoints_Edit" & vbCrLf & String.Join("", press.ToArray()) & vbCrLf &
            "AutoClaveDesktop:Integer Table.nTable_VacuumOnOff_Edit" & vbCrLf & String.Join("", vac.ToArray()) & vbCrLf &
            "AutoClaveDesktop:String Table.sTable_CureProfileName_Edit" & vbCrLf & "1:" & System.IO.Path.GetFileNameWithoutExtension(fileName) & vbCrLf & vbCrLf &
            "/ End of File"

        Return recipeString

    End Function

    Private Sub SaveFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveFileToolStripMenuItem.Click
        Call saveAs()
    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        Call SaveBtn_Click(sender, e)
    End Sub

    Private Sub MainForm_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If fileChanged = True Then
            If MsgBox("Would you like to save the current edits?", vbYesNo, "Save Check") = vbYes Then
                Call SaveBtn_Click(sender, e)
            End If
        End If
    End Sub

    Private Sub BtnNew_Click(sender As Object, e As EventArgs) Handles BtnNew.Click
        If fileChanged = True Then
            If MsgBox("Would you like to save the current edits?", vbYesNo, "Save Check") = vbYes Then
                Call SaveBtn_Click(sender, e)
            End If
        End If

        Application.Restart()
    End Sub

    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click
        BtnNew_Click(sender, e)
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        If fileChanged = True Then
            If MsgBox("Would you like to save the current edits?", vbYesNo, "Save Check") = vbYes Then
                Call SaveBtn_Click(sender, e)
            End If
        End If

        Application.Exit()
    End Sub
End Class
