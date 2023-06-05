Imports System.Data
Imports System.Data.OleDb
Public Class Discharge
    Public con As New OleDbConnection
    Public cmd As New OleDbCommand
    Public da As New OleDbDataAdapter
    Public ds As New DataSet
    Public query As String
    Dim wardname As String
    Public cn, b, pdct, t, d As String
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data source=|DataDirectory|\HMS_Database.accdb")
        con.Open()
        Using cmd As New OleDbCommand()
            cmd.CommandText = "Select * from PatientInfo where ID LIKE @ID + '%' "
            cmd.Connection = con
            cmd.Parameters.AddWithValue("@ID", TextBox1.Text.Trim())
            Dim dt As New DataTable()
            Using da As New OleDbDataAdapter(cmd)
                da.Fill(dt)
                If (dt.Rows.Count > 0) Then
                    DataGridView1.DataSource = dt
                    TextBox2.Text = dt.Rows(0)(1).ToString()
                    TextBox3.Text = dt.Rows(0)(4).ToString()
                    wardname = dt.Rows(0)(7).ToString()
                    DateTimePicker1.Text = dt.Rows(0)(6).ToString()
                    Noofdays()
                Else
                    MsgBox("Record Not Found", MsgBoxStyle.Critical, MsgBoxStyle.OkOnly)

                End If


            End Using
        End Using
        con.Close()



    End Sub

    Private Sub ShowBillDetail()


        Dim con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data source=|DataDirectory|\HMS_Database.accdb")
        con.Close()
        con.Open()
        Using cmd As New OleDbCommand()
            cmd.CommandText = "Select * from MedicineBill where Patient_ID LIKE @ID + '%' "
            cmd.Connection = con
            cmd.Parameters.AddWithValue("@ID", TextBox1.Text.Trim())
            Dim dt As New DataTable()
            Using da As New OleDbDataAdapter(cmd)
                da.Fill(dt)
                If (dt.Rows.Count > 0) Then
                    DataGridView2.DataSource = dt
                    '  TextBox6.Text = dt.Rows(0)(3).ToString()


                    Dim sum As Integer = 0
                    For i As Integer = 0 To DataGridView2.Rows.Count() - 1 Step +1
                        sum = sum + DataGridView2.Rows(i).Cells(3).Value
                    Next

                    TextBox6.Text = sum.ToString()


                    wardcharge()
                Else
                    MsgBox("Record Not Found", MsgBoxStyle.Critical, MsgBoxStyle.OkOnly)

                End If


            End Using
        End Using

    End Sub

    Public Sub Noofdays()
        

        Dim diff As Long = (DateTimePicker2.Value - DateTimePicker1.Value).Ticks
        diff = CLng(diff * 2.2) 'Multiply by a floating point number
        Dim tSpan As New TimeSpan(diff) 'Create new TimeSpan object
        'If (TextBox5.Text = tSpan.ToString() = "0") Then
        '    TextBox5.Text = 1

        'End If

        TextBox5.Text = tSpan.ToString()
        Dim aa As Integer
        aa = Val(TextBox5.Text)
        TextBox5.Text = aa
        ShowBillDetail()



    End Sub
    Public Sub wardcharge()

      

        Dim bb As String

        Dim con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data source=|DataDirectory|\HMS_Database.accdb")
        con.Close()
        con.Open()
        Using cmd As New OleDbCommand()
            cmd.CommandText = "Select * from ward where Name LIKE @ID + '%' "
            cmd.Connection = con
            cmd.Parameters.AddWithValue("@ID", wardname)
            Dim dt As New DataTable()
            Using da As New OleDbDataAdapter(cmd)
                da.Fill(dt)
                If (dt.Rows.Count > 0) Then
                    DataGridView3.DataSource = dt
                    '  TextBox6.Text = dt.Rows(0)(3).ToString()

                    bb = dt.Rows(0)(2).ToString()
                    If (TextBox5.Text = 0) Then
                        TextBox4.Text = 1 * Convert.ToInt32(bb)
                    Else
                        TextBox4.Text = Val(TextBox5.Text) * Convert.ToInt32(bb)
                    End If



                Else
                    MsgBox("Record Not Found", MsgBoxStyle.Critical, MsgBoxStyle.OkOnly)

                End If


            End Using
        End Using
















        
          
    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub TextBox7_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox7.TextChanged
        TextBox8.Text = Val(TextBox6.Text) + Val(TextBox4.Text) + Val(TextBox7.Text)
    End Sub
    Public Sub clr()
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox6.Clear()
        TextBox7.Clear()
        TextBox8.Clear()
        TextBox9.Clear()


      
    End Sub

    Private Sub Button2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click



        Dim con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data source=|DataDirectory|\HMS_Database.accdb")
        con.Open()
        query = "INSERT INTO [Discharge] ([Patient_ID],[Name],[Mobile],[AdmitDate],[OutDate],[MedicineBill],[WardUseDay],[WardCharge],[DoctorCharge],[Total],[ReceivedAmout]) values('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & DateTimePicker1.Value & "','" & DateTimePicker2.Value & "','" & TextBox6.Text & "','" & TextBox5.Text & "','" & TextBox4.Text & "','" & TextBox7.Text & "','" & TextBox8.Text & "','" & TextBox9.Text & "')"

        cmd = New OleDbCommand(query, con)
        Dim i As Integer = cmd.ExecuteNonQuery()
        If i > 0 Then
            MsgBox("Patient Discharge Successfully... ", MsgBoxStyle.Information)
            Bill()
            cn = TextBox2.Text
            t = TextBox8.Text
            d = DateTimePicker2.Value.ToString()

            clr()
            showdata()
            Me.Hide()
            Receipt.Show()
        End If
        con.Close()

    End Sub

    Private Sub showdata()
        Dim con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data source=|DataDirectory|\HMS_Database.accdb")
        con.Open()

        query = "Select * From Discharge"

        da = New OleDbDataAdapter(query, con)
        ds = New DataSet
        da.Fill(ds)

        If (ds.Tables(0).Rows.Count > 0) Then
            DataGridView4.DataSource = ds.Tables(0)

        Else
            MsgBox("Record Not Found", MsgBoxStyle.Critical)
        End If
        'con.Close()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Close()

    End Sub

    Private Sub Discharge_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data source=|DataDirectory|\HMS_Database.accdb")
        showdata()
    End Sub
    Public Sub Bill()
        Dim sql As String = "select max(ID) As BillNO from Discharge"
        
        Dim newEmpId As Int32 = 0
        Using conn As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data source=|DataDirectory|\HMS_Database.accdb")
            Dim cmd As New OleDbCommand(sql, conn)
            Try
                conn.Open()
                newEmpId = Convert.ToInt32(cmd.ExecuteScalar())
            Catch ex As Exception
                Console.WriteLine(ex.Message)
            End Try
        End Using
        b = newEmpId

    End Sub
End Class