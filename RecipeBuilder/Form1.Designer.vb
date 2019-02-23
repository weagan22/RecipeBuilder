<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Segment = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Time = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Temp = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Pressure = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.VacuumToggle = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Segment, Me.Time, Me.Temp, Me.Pressure, Me.VacuumToggle})
        Me.DataGridView1.Location = New System.Drawing.Point(58, 50)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(590, 162)
        Me.DataGridView1.TabIndex = 0
        '
        'Segment
        '
        Me.Segment.HeaderText = "Segment #"
        Me.Segment.Name = "Segment"
        Me.Segment.ReadOnly = True
        '
        'Time
        '
        Me.Time.HeaderText = "Time (min)"
        Me.Time.Name = "Time"
        '
        'Temp
        '
        Me.Temp.HeaderText = "Temperature (F)"
        Me.Temp.Name = "Temp"
        '
        'Pressure
        '
        Me.Pressure.HeaderText = "Pressure (psi)"
        Me.Pressure.Name = "Pressure"
        '
        'VacuumToggle
        '
        Me.VacuumToggle.HeaderText = "Vacuum"
        Me.VacuumToggle.Name = "VacuumToggle"
        Me.VacuumToggle.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(718, 288)
        Me.Controls.Add(Me.DataGridView1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Segment As DataGridViewTextBoxColumn
    Friend WithEvents Time As DataGridViewTextBoxColumn
    Friend WithEvents Temp As DataGridViewTextBoxColumn
    Friend WithEvents Pressure As DataGridViewTextBoxColumn
    Friend WithEvents VacuumToggle As DataGridViewCheckBoxColumn
End Class
