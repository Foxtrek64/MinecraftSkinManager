Public Class Form1

    Public Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Application.DoEvents()
        ListBox1.Items.Insert(0, "Minecraft Skin Downloader")
        ListBox1.Items.Insert(0, "Written by Foxtrek_64")
        ListBox1.Items.Insert(0, "Skins come from https://minotar.net/")
        ListBox1.Items.Insert(0, "Select a skin before saving")

    End Sub
    Public Function UpdateImage(ByVal _Image As Image) As Boolean
        If _Image IsNot Nothing Then
            PictureBox1.Image = _Image
            ListBox1.Items.Insert(0, "Updating Preview")
        ElseIf _Image Is Nothing Then
            PictureBox1.Image = My.Resources.steve
        End If
        Return True

    End Function

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        ' Save Button
        If TextBox1.Text = "" Then
            ListBox1.Items.Insert(0, "There is no Username specified.")
        ElseIf TextBox1.Text <> "" Then

            Dim SaveFileDialog1 As New SaveFileDialog()
            SaveFileDialog1.FileName = TextBox1.Text
            SaveFileDialog1.AddExtension = True
            SaveFileDialog1.Title = "Save As..."
            SaveFileDialog1.Filter = "PNG Image (*.png)|*.png"
            SaveFileDialog1.ShowDialog()

            Dim _FileStream As System.IO.FileStream = CType _
                (SaveFileDialog1.OpenFile(), System.IO.FileStream)

            Me.PictureBox1.Image.Save(_FileStream, _
            System.Drawing.Imaging.ImageFormat.Png)
            _FileStream.Flush()

            _FileStream.Close()

            ListBox1.Items.Insert(0, "Save Successful.")

        End If

    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        ' Use Skin Button
        Dim Location As String = "https://minecraft.net/profile/skin/remote?url=https://minotar.net/skin/" & TextBox1.Text
        Process.Start(Location)
    End Sub

    Public Function DownloadImage(ByVal _URL As String) As Image
        Dim _tmpImage As Image = Nothing

        Try
            ' Open a connection
            ListBox1.Items.Insert(0, "Checking Skin...")
            Dim _HttpWebRequest As System.Net.HttpWebRequest = CType(System.Net.HttpWebRequest.Create(_URL), System.Net.HttpWebRequest)

            _HttpWebRequest.AllowWriteStreamBuffering = True

            ' You can also specify additional header values like the user agent or the referer: (Optional)
            _HttpWebRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1)"
            _HttpWebRequest.Referer = "http://www.google.com/"

            ' set timeout for 20 seconds (Optional)
            _HttpWebRequest.Timeout = 20000

            ' Request response:
            Dim _WebResponse As System.Net.WebResponse = _HttpWebRequest.GetResponse()

            ' Open data stream:
            Dim _WebStream As System.IO.Stream = _WebResponse.GetResponseStream()

            ' convert webstream to image
            _tmpImage = Image.FromStream(_WebStream)

            ' Cleanup
            _WebResponse.Close()
            _WebResponse.Close()

            ' File Found
            Console.WriteLine("File Found with name " & TextBox1.Text & ".png")
            ListBox1.Items.Insert(0, "File Found with name " & TextBox1.Text & ".png")
            Button1.Enabled = True
            Button2.Enabled = True
        Catch _Exception As Exception
            ' Error
            Console.WriteLine("Error: File not found at " & TextBox1.Text, _Exception.ToString())
            ListBox1.Items.Insert(0, "Error: File not found at " & TextBox1.Text)
            Button1.Enabled = False
            Button2.Enabled = False
            Return Nothing
        End Try

        Return _tmpImage
    End Function


    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        ' Search Button
        Dim _Image As Image = Nothing
        Dim Location As String = "https://minotar.net/skin/" & TextBox1.Text
        If TextBox1.Text <> "" Then
            _Image = DownloadImage(Location)
        Else
            _Image = Nothing
            ListBox1.Items.Insert(0, "There is no Username specified.")
        End If
        UpdateImage(_Image)
    End Sub

End Class
