Imports System.Data
Imports System.Data.OleDb
Public Class AddMedicineBill
    Public con As New OleDbConnection
    Public cmd As New OleDbCommand
    Public da As New OleDbDataAdapter
    Public ds As New DataSet
    Public query As String
    Dim str As String

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data source=|DataDirectory|\HMS_Database.accdb")
        con.Open()
        query = "INSERT INTO [MedicineBill] ([Patient_ID],[Particular],[Amount],[Date]) values('" & ComboBox1.Text & "','" & TextBox1.Text & "','" & TextBox2.Text & "','" & DateTimePicker1.Value & "')"

        cmd = New OleDbCommand(query, con)
        Dim i As Integer = cmd.ExecuteNonQuery()
        If i > 0 Then
            MsgBox("Record  Save")
            TextBox1.Text = ""
            TextBox2.Text = ""
            ComboBox1.SelectedIndex = -1



        End If
        con.Close()
    End Sub

    Private Sub AddMedicineBill_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PatientID()





       

    End Sub
    Public Sub PatientID()
        Try
            Dim con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data source=|DataDirectory|\HMS_Database.accdb")
            con.Open()
            str = "Select * from AllotWard"
            da = New OleDb.OleDbDataAdapter(str, con)

            ds = New DataSet
            da.Fill(ds, "AllotWard")
            With ComboBox1
                .DataSource = ds.Tables(0)
                .DisplayMember = "Patient_ID"
                .ValueMember = "ID"

            End With


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        con.Close()

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Close()

    End Sub
End Class