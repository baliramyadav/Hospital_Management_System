Imports System.Data
Imports System.Data.OleDb
Public Class Allotward
    Public con As New OleDbConnection
    Public cmd As New OleDbCommand
    Public da As New OleDbDataAdapter
    Public ds As New DataSet
    Public query As String
    Private Sub Allotward_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data source=|DataDirectory|\HMS_Database.accdb")
        PatientShow()
        Wardshow()
    End Sub
    Public Sub PatientShow()

        Dim con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data source=|DataDirectory|\HMS_Database.accdb")
        con.Open()

        query = "Select ID,Name From PatientInfo "

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
    Public Sub Wardshow()

        Dim con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data source=|DataDirectory|\HMS_Database.accdb")
        con.Open()

        query = "Select * From Ward"

        da = New OleDbDataAdapter(query, con)
        ds = New DataSet
        da.Fill(ds)

        If (ds.Tables(0).Rows.Count > 0) Then
            DataGridView2.DataSource = ds.Tables(0)

        Else
            MsgBox("Record Not Found", MsgBoxStyle.Critical)
        End If
        'con.Close()

    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim i As Integer

        i = DataGridView1.CurrentRow.Index

        Me.TextBox1.Text = DataGridView1.Item(0, i).Value
       
    End Sub

    Private Sub DataGridView2_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        Dim j As Integer

        j = DataGridView2.CurrentRow.Index

        Me.TextBox2.Text = DataGridView2.Item(1, j).Value
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Close()

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data source=|DataDirectory|\HMS_Database.accdb")
        con.Open()
        query = "INSERT INTO [AllotWard] ([Patient_ID],[WardName],[DateIN],[Disease]) values('" & TextBox1.Text & "','" & TextBox2.Text & "','" & DateTimePicker1.Value & "','" & TextBox3.Text & "')"

        cmd = New OleDbCommand(query, con)
        Dim i As Integer = cmd.ExecuteNonQuery()
        If i > 0 Then
            MsgBox("Record  Save")
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""


        End If
        con.Close()
    End Sub
End Class