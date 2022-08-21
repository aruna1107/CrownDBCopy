Imports MySql.Data.MySqlClient

Public Class Form1
    Dim OldDB = "server='220.247.245.171';port='3117';database='ek_crown';uid='root';pwd='myPass#word1';Convert Zero Datetime=True;"
    ' Dim NEWDB = "server='220.247.245.171';port='3117';database='ek_crowndubai';uid='root';pwd='myPass#word1';Convert Zero Datetime=True;"
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim Q = "select table_Name from Information_schema.tables where table_schema='EK_crown' and Table_type='View'"
        FK_FillGridOld(Q, dgv)
    End Sub

    Public Sub FK_FillGridOld(ByVal SQL As String, ByVal DataGridViewName As DataGridView)
        Cursor.Current = Cursors.WaitCursor
        Using CN As New MySqlConnection(OldDB)
            Dim sBol As Boolean = False
            Try
                CN.Open()
                Dim ADP As New MySqlDataAdapter
                Dim sTable As New DataSet
                ADP = New MySqlDataAdapter(SQL, CN)
                ADP.Fill(sTable)
                DataGridViewName.DataSource = sTable.Tables(0)
                For iROW = 0 To DataGridViewName.Columns.Count - 1
                    DataGridViewName.Columns(iROW).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                Next
                For iROW = 0 To DataGridViewName.Columns.Count - 1
                    DataGridViewName.Columns(iROW).HeaderText = UCase(DataGridViewName.Columns(iROW).HeaderText)
                Next
                Cursor.Current = Cursors.Default
            Catch ex As Exception
                Cursor.Current = Cursors.Default
                MsgBox(ex.Message)
            End Try
            CN.Close()
        End Using
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        For iR = 0 To dgv.RowCount - 1
            Dim sTable = dgv.Item(0, iR).Value
            Dim Q = "Show Create table " & sTable
            Dim sOut As String = FK_Retstring(Q)

            TextBox1.Text = TextBox1.Text & vbCrLf & sOut & ";"
            ' Exit Sub
        Next
        MsgBox("Process Completed Successfully", MsgBoxStyle.Information)
    End Sub
    Public Function FK_Retstring(ByVal qry As String) As String

        Dim strvalue As String = ""
        Using sqlcon As New MySqlConnection(OldDB)
            Try
                sqlcon.Open()
                Dim sqlcomnd As New MySqlCommand(qry, sqlcon)
                Dim sqlreader As MySqlDataReader = sqlcomnd.ExecuteReader

                If sqlreader.Read Then
                    strvalue = sqlreader.Item(1).ToString
                Else
                    strvalue = ""
                End If
                sqlreader.Close()
            Catch ex As Exception
                MsgBox("error occured while reading the database. " + ex.Message)
            Finally
                sqlcon.Close()

            End Try
        End Using
        Return strvalue

    End Function

End Class
