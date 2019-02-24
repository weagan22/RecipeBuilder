Public Class MainForm

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub



    Private Sub DataGridView1_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles DataGridView1.RowsAdded
        If DataGridView1.RowCount > 2 Then 'Copies values from above to fill blanks in table
            For z = 1 To DataGridView1.ColumnCount - 1
                DataGridView1.Item(z, DataGridView1.RowCount - 2).Value = DataGridView1.Item(z, DataGridView1.RowCount - 3).Value
            Next
        End If
    End Sub

    Private Sub DataGridView1_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged

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

        If e.ColumnIndex = 3 Then 'Set max pressure
            If DataGridView1.Item(e.ColumnIndex, e.RowIndex).Value > 200 Then
                Me.ToolStripStatusLabel1.Text = "Error: Max pressure is 200 psi."
                DataGridView1.Item(e.ColumnIndex, e.RowIndex).Value = 200
            End If
        End If

        If DataGridView1.RowCount > 1 Then 'Enter values into the chart if there is more that 1 row

            For i = 0 To Chart1.Series.Count - 1 'Clear all of the previously entered values in the chart
                If Chart1.Series.Item(i).Points.Count > 0 Then
                    Chart1.Series.Item(i).Points.Clear()
                End If

                Chart1.Series.Item(i).Points.AddXY(0, 0) 'Add a start point each series of the chart
            Next

            Dim xVal As Integer = 0

            For i = 0 To DataGridView1.RowCount - 2 'Add values for each row in the table
                Dim yVal As Integer = 0

                yVal = DataGridView1.Item(4, i).Value
                Chart1.Series.Item(2).Points.AddXY(xVal, yVal * -50) 'Input vacuum data


                xVal = xVal + DataGridView1.Item(1, i).Value 'Add current time to xVal


                yVal = DataGridView1.Item(2, i).Value
                Chart1.Series.Item(0).Points.AddXY(xVal, yVal) 'Input temperature data

                yVal = DataGridView1.Item(3, i).Value
                Chart1.Series.Item(1).Points.AddXY(xVal, yVal) 'Input pressure data

                yVal = DataGridView1.Item(4, i).Value
                Chart1.Series.Item(2).Points.AddXY(xVal, yVal * -50) 'Input vacuum data
            Next
        End If
    End Sub

    Private Sub OpenBtn_Click(sender As Object, e As EventArgs) Handles OpenBtn.Click
        If OpenFileDialog.ShowDialog() = DialogResult.OK Then
            MsgBox("DataGridViewHitTestType")
        End If

    End Sub

    Private Sub DataGridView1_RowStateChanged(sender As Object, e As DataGridViewRowStateChangedEventArgs) Handles DataGridView1.RowStateChanged
        If DataGridView1.RowCount > 0 Then 'Adds segment numbers to table
            For i = 0 To DataGridView1.RowCount - 1
                DataGridView1.Item(0, i).Value = i + 1
            Next
        End If

        For i = 0 To DataGridView1.RowCount - 1 'Fill all blank cells with 0
            For z = 1 To DataGridView1.ColumnCount - 2
                If Not IsNumeric(DataGridView1.Item(z, i).Value) Then
                    DataGridView1.Item(z, i).Value = 0
                End If
            Next
        Next
    End Sub

    Private Sub OpenFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenFileToolStripMenuItem.Click
        OpenBtn.PerformClick()
    End Sub
End Class
