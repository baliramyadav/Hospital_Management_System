Imports System.Data
Imports System.Data.OleDb
Public Class AddPatient
    Public con As New OleDbConnection
    Public cmd As New OleDbCommand
    Public da As New OleDbDataAdapter
    Public ds As New DataSet
    Public query As String
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data source=|DataDirectory|\HMS_Database.accdb")
        con.Open()
        query = "INSERT INTO [PatientInfo] ([Name],[Gender],[Age],[Mobile],[Address],[AdmitDate],[Ward]) values('" & TextBox1.Text & "','" & ComboBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "','" & DateTimePicker1.Value & "','" & ComboBox2.Text & "')"

        cmd = New OleDbCommand(query, con)
        Dim i As Integer = cmd.ExecuteNonQuery()
        If i > 0 Then
            MsgBox("Record  Save")
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox4.Text = ""
            showdata()
        End If
        con.Close()

    End Sub
    Public Sub showdata()

        Dim con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data source=|DataDirectory|\HMS_Database.accdb")
        con.Open()

        query = "Select * From PatientInfo"

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

    Private Sub AddPatient_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data source=|DataDirectory|\HMS_Database.accdb")
        Button1.Enabled = False
        Button2.Enabled = False
        Button3.Enabled = False


        showdata()

    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim i As Integer

        i = DataGridView1.CurrentRow.Index

        Me.TextBox5.Text = DataGridView1.Item(0, i).Value
        Me.TextBox1.Text = DataGridView1.Item(1, i).Value
        Me.ComboBox1.Text = DataGridView1.Item(2, i).Value
        Me.TextBox2.Text = DataGridView1.Item(3, i).Value
        Me.TextBox3.Text = DataGridView1.Item(4, i).Value
        Me.TextBox4.Text = DataGridView1.Item(5, i).Value
        Me.DateTimePicker1.Text = DataGridView1.Item(6, i).Value
        Me.ComboBox2.Text = DataGridView1.Item(7, i).Value



        Button1.Enabled = False
        Button2.Enabled = True
        Button3.Enabled = True

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Close()

    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Button2.Enabled = False
        Button3.Enabled = False
        Button1.Enabled = True
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        ComboBox1.SelectedIndex = -1
        ComboBox2.SelectedIndex = -1
        TextBox1.Focus()


    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data source=|DataDirectory|\HMS_Database.accdb")
        con.Open()
        query = " UPDATE [PatientInfo] set [Name]='" & TextBox1.Text & "',[Gender]='" & ComboBox1.Text & "',[Age]='" & TextBox2.Text & "',[Mobile]='" & TextBox3.Text & "',[Address]='" & TextBox4.Text & "',[AdmitDate]='" & DateTimePicker1.Value & "',[Ward]='" & ComboBox2.Text & "' where ID=" & Convert.ToInt32(TextBox5.Text) & ""

        cmd = New OleDbCommand(query, con)
        Dim i As Integer = cmd.ExecuteNonQuery()
        If i > 0 Then
            MsgBox("Record Updated...")
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox4.Text = ""
            TextBox5.Text = ""
            ComboBox1.SelectedIndex = -1
            ComboBox2.SelectedIndex = -1
            DateTimePicker1.Text = String.Empty
            showdata()
        End If
        con.Close()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data source=|DataDirectory|\HMS_Database.accdb")
        con.Open()
        query = "DELETE From [PatientInfo] where ID=" & Convert.ToInt32(TextBox5.Text) & ""
        cmd = New OleDbCommand(query, con)
        Dim i As Integer = cmd.ExecuteNonQuery()
        If i > 0 Then
            MsgBox("Record Deleted...")
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox4.Text = ""
            TextBox5.Text = ""
            ComboBox1.SelectedIndex = -1
            ComboBox2.SelectedIndex = -1

            showdata()
        End If
        con.Close()
    End Sub
End Class