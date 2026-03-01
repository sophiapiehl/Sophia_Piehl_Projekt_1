Imports System.IO

Module Program

    Structure Book
        Public ISBN As String
        Public Title As String
        Public Author As String
        Public Status As String
    End Structure

    Structure User
        Public ID As String
        Public Name As String
    End Structure

    Dim books() As Book
    Dim users() As User

    Sub Main()

        ' ---------------------------
        ' Bücher einlesen
        ' ---------------------------

        Dim bookLines() As String = File.ReadAllLines("library_books.csv")

        ' Header nicht mitzählen
        ReDim books(bookLines.Length - 2)

        Dim bookIndex As Integer = 0

        For i As Integer = 1 To bookLines.Length - 1

            If bookLines(i).Trim() <> "" Then

                Dim parts() As String = bookLines(i).Split(","c)

                If parts.Length = 4 Then
                    books(bookIndex).ISBN = parts(0).Trim()
                    books(bookIndex).Title = parts(1).Trim()
                    books(bookIndex).Author = parts(2).Trim()
                    books(bookIndex).Status = parts(3).Trim()

                    bookIndex += 1
                End If

            End If

        Next


        ' ---------------------------
        ' Benutzer einlesen
        ' ---------------------------

        Dim userLines() As String = File.ReadAllLines("library_users.csv")

        ReDim users(userLines.Length - 2)

        Dim userIndex As Integer = 0

        For i As Integer = 1 To userLines.Length - 1

            If userLines(i).Trim() <> "" Then

                Dim parts() As String = userLines(i).Split(","c)

                If parts.Length = 2 Then
                    users(userIndex).ID = parts(0).Trim()
                    users(userIndex).Name = parts(1).Trim()

                    userIndex += 1
                End If

            End If

        Next


        ' ---------------------------
        ' Kontrolle
        ' ---------------------------

        Console.WriteLine("Bücher erfolgreich geladen: " & bookIndex)
        Console.WriteLine("Benutzer erfolgreich geladen: " & userIndex)

        Console.ReadLine()

        Dim running As Boolean = True

        While running

            Console.WriteLine("===== Bibliothekssystem =====")
            Console.WriteLine("1 - Alle Bücher anzeigen")
            Console.WriteLine("2 - Alle Benutzer anzeigen")
            Console.WriteLine("0 - Beenden")
            Console.Write("Auswahl: ")

            Dim input As String = Console.ReadLine().Trim()

            If input = "1" Then
                For i As Integer = 0 To books.Length - 1
                    Console.WriteLine(books(i).ISBN & " | " &
                                      books(i).Title & " | " &
                                      books(i).Author & " | " &
                                      books(i).Status)
                Next

            ElseIf input = "2" Then
                For i As Integer = 0 To users.Length - 1
                    Console.WriteLine(users(i).ID & " | " &
                                      users(i).Name)
                Next

            ElseIf input = "0" Then
                running = False

            Else
                Console.WriteLine("Ungültige Eingabe.")
            End If

            Console.WriteLine()

        End While

    End Sub
End Module