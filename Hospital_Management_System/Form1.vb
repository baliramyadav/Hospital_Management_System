Imports System.Data
Imports System.Data.OleDb


Public Class Form1
    Public cmd As New OleDbCommand
    Public da As New OleDbDataAdapter
    Public ds As New DataSet
    Public query As String
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        End

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data source=|DataDirectory|\HMS_Database.accdb") '"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\DellPC\Documents\HMS_Database.accdb"
        con.Open()
        query = "Select * from Login where UserName='" & TextBox1.Text & "' and Password='" & TextBox2.Text & "'"
        cmd = New OleDbCommand(query, con)
        Dim dr As OleDbDataReader = cmd.ExecuteReader()
        If dr.HasRows Then


            con.Close()



            Home.Show()
            Me.Hide()
        Else
            MsgBox("UserID & Password Not Matched", MsgBoxStyle.Critical, MsgBoxStyle.OkOnly)
        End If


    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Focus()

    End Sub
End Class
