Imports System.Data
Imports System.Data.OleDb
Public Class AddDoctor
    Public query As String
    Public cmd As New OleDbCommand
    Public da As New OleDbDataAdapter
    Public ds As New DataSet
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Dim con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=D:\HMS_Database.accdb")
        Dim con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data source=|DataDirectory|\HMS_Database.accdb")
        con.Open()
        query = "INSERT INTO [Doctor]([Name],[Address],[Phone],[Department]) values('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "')"
        cmd = New OleDbCommand(query, con)
        Dim i As Integer = cmd.ExecuteNonQuery()
        If i > 0 Then
            MsgBox("Record  Save")
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox4.Text = ""

        End If
        
        con.Close()

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()

    End Sub
End Class