Public Class Form1

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ListBox1.Items.Add(InputBox("請輸入欲新增的指令"))
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim ifdelete As UShort
        ifdelete = MsgBox("是否要刪除該指令:" & vbCrLf & ListBox1.Text, MsgBoxStyle.YesNo)
        If ifdelete = 6 Then ListBox1.Items.Remove(ListBox1.Text)
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged, Me.Load
        If ListBox1.SelectedIndex = -1 Then
            Button4.Enabled = False
            Button5.Enabled = False
            Button6.Enabled = False
            Button7.Enabled = False
        Else
            Button4.Enabled = True
            Button5.Enabled = True
            If Not ListBox1.Items.Count = 1 And Not ListBox1.SelectedIndex = 0 Then
                Button6.Enabled = True
            Else
                Button6.Enabled = False
            End If
            If Not ListBox1.Items.Count = 1 And Not ListBox1.SelectedIndex + 1 = ListBox1.Items.Count Then
                Button7.Enabled = True
            Else
                Button7.Enabled = False
            End If
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click, ListBox1.DoubleClick
        If ListBox1.SelectedIndex = -1 Then
            Exit Sub
        End If
        Dim newcommand As String
        Dim index As Integer
        newcommand = InputBox("請輸入新指令")
        If newcommand <> "" Then
            index = ListBox1.SelectedIndex
            ListBox1.Items.RemoveAt(index)
            ListBox1.Items.Insert(index, newcommand)
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim text1, text2 As String
        Dim index As Integer
        index = ListBox1.SelectedIndex
        text1 = ListBox1.Items.Item(index - 1)
        text2 = ListBox1.Items.Item(index)

        ListBox1.Items.RemoveAt(index - 1)
        ListBox1.Items.Insert(index - 1, text2)

        ListBox1.Items.RemoveAt(index)
        ListBox1.Items.Insert(index, text1)

        ListBox1.SelectedIndex = index - 1
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim text1, text2 As String
        Dim index As Integer
        index = ListBox1.SelectedIndex
        text1 = ListBox1.Items.Item(index)
        text2 = ListBox1.Items.Item(index + 1)

        ListBox1.Items.RemoveAt(index + 1)
        ListBox1.Items.Insert(index + 1, text1)

        ListBox1.Items.RemoveAt(index)
        ListBox1.Items.Insert(index, text2)

        ListBox1.SelectedIndex = index + 1
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        If ListBox1.Items.Count = 0 Then
            MsgBox("嗯...請先加幾個指令再用此功能吧!")
        End If
        If ListBox1.Items.Count = 1 Then
            TextBox1.Text = "summon MinecartCommandBlock " & TextBox2.Text & " " & TextBox3.Text & " " & TextBox4.Text & " {Command:" & """" & ListBox1.Items.Item(0) & """" & "}"
        End If
        If ListBox1.Items.Count > 1 Then
            TextBox1.Text = "summon MinecartCommandBlock " & TextBox2.Text & " " & TextBox3.Text & " " & TextBox4.Text & " "
            For i = 0 To ListBox1.Items.Count - 2
                TextBox1.Text = TextBox1.Text & "{Riding:"
            Next

            '第一個指令
            TextBox1.Text = TextBox1.Text & "{id:MinecartCommandBlock,Command:" & """" & ListBox1.Items.Item(0) & """" & "}"

            '第二 ~ 倒數第二個指令
            For i = 1 To ListBox1.Items.Count - 2
                TextBox1.Text = TextBox1.Text & ",id:MinecartCommandBlock,Command:" & """" & ListBox1.Items.Item(i) & """" & "}"
            Next

            '最後一個指令
            TextBox1.Text = TextBox1.Text & ",Command:" & """" & ListBox1.Items.Item(ListBox1.Items.Count - 1) & """" & "}"
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If ListBox1.Items.Count = 0 Then
            MsgBox("嗯...請先加幾個指令再用此功能吧!")
            Exit Sub
        End If
        SaveFileDialog1.ShowDialog()
        If SaveFileDialog1.FileName = "" Then Exit Sub
        If My.Computer.FileSystem.FileExists(SaveFileDialog1.FileName) Then My.Computer.FileSystem.DeleteFile(SaveFileDialog1.FileName)
        Dim commands As String = ""
        For i = 0 To ListBox1.Items.Count - 1
            If i = ListBox1.Items.Count - 1 Then
                commands = commands & ListBox1.Items.Item(i)
            Else
                commands = commands & ListBox1.Items.Item(i) & vbCrLf
            End If
        Next
        My.Computer.FileSystem.WriteAllText(SaveFileDialog1.FileName, TextBox2.Text & vbCrLf & TextBox3.Text & vbCrLf & TextBox4.Text & vbCrLf & commands, False)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        OpenFileDialog1.ShowDialog()
        If OpenFileDialog1.FileName = "" Then Exit Sub
        If Not My.Computer.FileSystem.FileExists(OpenFileDialog1.FileName) Then
            MsgBox("指定了不存在的檔案  或是使用者取消動作")
            Exit Sub
        End If
        ListBox1.Items.Clear()
        Dim allline As String() = System.IO.File.ReadAllLines(OpenFileDialog1.FileName)
        TextBox2.Text = allline(0)
        TextBox3.Text = allline(1)
        TextBox4.Text = allline(2)
        For i = 3 To allline.Length - 1
            ListBox1.Items.Add(allline(i))
        Next
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged, Me.Load
        If TextBox1.Text = "" Then
            Button9.Enabled = False
        Else
            Button9.Enabled = True
        End If
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        My.Computer.Clipboard.SetText(TextBox1.Text)
    End Sub
End Class
