Public Class MainForm
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Chart1.Series(0).ChartType = DataVisualization.Charting.SeriesChartType.Line
    End Sub
End Class
