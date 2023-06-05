Imports System.Data
Imports System.Data.OleDb
Public Class AddWard
    Public con As New OleDbConnection
    Public query As String
    Public cmd As New OleDbCommand
    Public da As New OleDbDataAdapter
    Public ds As New DataSet
    Private Sub AddWard_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data source=|DataDirectory|\HMS_Database.accdb")
        Button1.Enabled = False
        Button2.Enabled = False
        Button3.Enabled = False
        TextBox1.Enabled = False

        TextBox2.Enabled = False
        TextBox3.Enabled = False
        showdata()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        TextBox1.Enabled = True

        TextBox2.Enabled = True
        TextBox3.Enabled = True
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox2.Focus()
        Button2.Enabled = False
        Button3.Enabled = False
        Button1.Enabled = True

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Close()

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data source=|DataDirectory|\HMS_Database.accdb")
        con.Open()
        query = "INSERT INTO [Ward] ([Name],[Charge]) values('" & TextBox2.Text & "','" & TextBox3.Text & "')"

        cmd = New OleDbCommand(query, con)
        Dim i As Integer = cmd.ExecuteNonQuery()
        If i > 0 Then
            MsgBox("Record  Save")
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""

            showdata()
        End If
        con.Close()
    End Sub
    Public Sub showdata()

        Dim con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data source=|DataDirectory|\HMS_Database.accdb")
        con.Open()

        query = "Select * From Ward"

        da = New OleDbDataAdapter(query, con)
        ds = New DataSet
        da.Fill(ds)

        If (ds.Tables(0).Rows.Count > 0) Then
            DataGridView1.DataSource = ds.Tables(0)

        Else
            MsgBox("Record Not Found", MsgBoxStyle.Critical)
        End If
        'con.Close()

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data source=|DataDirectory|\HMS_Database.accdb")
        con.Open()
        query = " UPDATE [Ward] set [Name]='" & TextBox2.Text & "',[Charge]='" & TextBox3.Text & "' where ID=" & Convert.ToInt32(TextBox1.Text) & ""

        cmd = New OleDbCommand(query, con)
        Dim i As Integer = cmd.ExecuteNonQuery()
        If i > 0 Then
            MsgBox("Record Updated...")
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            
            showdata()
        End If
        con.Close()
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim i As Integer

        i = DataGridView1.CurrentRow.Index

        Me.TextBox1.Text = DataGridView1.Item(0, i).Value
        Me.TextBox2.Text = DataGridView1.Item(1, i).Value
        Me.TextBox3.Text = DataGridView1.Item(2, i).Value
        

        TextBox1.Enabled = True
        TextBox2.Enabled = True
        TextBox3.Enabled = True


        Button1.Enabled = False
        Button2.Enabled = True
        Button3.Enabled = True

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click

        Dim con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data source=|DataDirectory|\HMS_Database.accdb")
        con.Open()
        query = "DELETE From [Ward] where ID=" & Convert.ToInt32(TextBox1.Text) & ""
        cmd = New OleDbCommand(query, con)
        Dim i As Integer = cmd.ExecuteNonQuery()
        If i > 0 Then
            MsgBox("Record Deleted...")
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
           

            showdata()
        End If
        con.Close()
    End Sub
End Class