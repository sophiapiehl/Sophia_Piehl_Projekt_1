Imports System.IO

Module Program

    ' Struktur für Bücher
    Structure Book
        Public ISBN As String
        Public Title As String
        Public Author As String
        Public Status As String
    End Structure

    ' Struktur für Benutzer
    Structure User
        Public ID As String
        Public Name As String
    End Structure

    Dim books() As Book
    Dim users() As User

    ''' <summary>
    ''' Hauptprogramm. Lädt Daten und startet das Menü.
    ''' </summary>
    Sub Main()

        LoadBooks()
        LoadUsers()

        Dim running As Boolean = True

        While running

            ShowMenu()

            Dim input As String = Console.ReadLine().Trim()

            If input = "1" Then
                ShowBooks()

            ElseIf input = "2" Then
                ShowUsers()

            ElseIf input = "3" Then
                BorrowBook()

            ElseIf input = "4" Then
                ReturnBook()

            ElseIf input = "5" Then
                CreateUser()

            ElseIf input = "6" Then
                ShowBorrowedBooksOfUser()

            ElseIf input = "0" Then
                running = False

            Else
                Console.WriteLine("Ungültige Eingabe.")
            End If

            Console.WriteLine()

        End While

    End Sub


    ''' <summary>
    ''' Zeigt das Menü an.
    ''' </summary>
    Sub ShowMenu()

        Console.WriteLine("===== Bibliothekssystem =====")
        Console.WriteLine("1 - Alle Bücher anzeigen")
        Console.WriteLine("2 - Alle Benutzer anzeigen")
        Console.WriteLine("3 - Buch ausleihen")
        Console.WriteLine("4 - Buch zurückgeben")
        Console.WriteLine("5 - Benutzer anlegen")
        Console.WriteLine("6 - Ausgeliehene Bücher eines Benutzers anzeigen")
        Console.WriteLine("0 - Beenden")
        Console.Write("Auswahl: ")

    End Sub


    ''' <summary>
    ''' Liest Bücher aus der CSV-Datei ein.
    ''' </summary>
    Sub LoadBooks()

        Dim bookLines() As String = File.ReadAllLines("library_books.csv")

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

    End Sub


    ''' <summary>
    ''' Liest Benutzer aus der CSV-Datei ein.
    ''' </summary>
    Sub LoadUsers()

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

    End Sub


    ''' <summary>
    ''' Zeigt alle Bücher an.
    ''' </summary>
    Sub ShowBooks()

        For i As Integer = 0 To books.Length - 1
            Console.WriteLine(books(i).ISBN & " | " &
                              books(i).Title & " | " &
                              books(i).Author & " | " &
                              books(i).Status)
        Next

    End Sub


    ''' <summary>
    ''' Zeigt alle Benutzer an.
    ''' </summary>
    Sub ShowUsers()

        For i As Integer = 0 To users.Length - 1
            Console.WriteLine(users(i).ID & " | " &
                              users(i).Name)
        Next

    End Sub


    ''' <summary>
    ''' Verleiht ein Buch an einen Benutzer.
    ''' </summary>
    Sub BorrowBook()

        Console.Write("Benutzer-ID eingeben: ")
        Dim userId As String = Console.ReadLine().Trim()

        Console.Write("ISBN eingeben: ")
        Dim isbn As String = Console.ReadLine().Trim()

        Dim userFound As Boolean = False
        Dim bookFound As Boolean = False

        ' Benutzer suchen
        For i As Integer = 0 To users.Length - 1
            If users(i).ID = userId Then
                userFound = True
                Exit For
            End If
        Next

        ' Wenn Benutzer nicht existiert -> abbrechen
        If Not userFound Then
            Console.WriteLine("Benutzer nicht gefunden.")
            Return
        End If

        ' Buch suchen
        For i As Integer = 0 To books.Length - 1
            If books(i).ISBN = isbn Then
                bookFound = True

                If books(i).Status = "available" Then
                    books(i).Status = "borrowed"
                    Console.WriteLine("Buch erfolgreich ausgeliehen.")
                Else
                    Console.WriteLine("Buch ist bereits ausgeliehen.")
                End If

                Exit For
            End If
        Next

        If Not bookFound Then
            Console.WriteLine("Buch nicht gefunden.")
        End If

    End Sub


    ''' <summary>
    ''' Gibt ein Buch zurück.
    ''' </summary>
    Sub ReturnBook()

        Console.Write("ISBN eingeben: ")
        Dim isbn As String = Console.ReadLine().Trim()

        Dim bookFound As Boolean = False

        For i As Integer = 0 To books.Length - 1

            If books(i).ISBN = isbn Then
                bookFound = True

                If books(i).Status = "borrowed" Then
                    books(i).Status = "available"
                    Console.WriteLine("Buch erfolgreich zurückgegeben.")
                Else
                    Console.WriteLine("Buch war nicht ausgeliehen.")
                End If

                Exit For
            End If

        Next

        If Not bookFound Then
            Console.WriteLine("Buch nicht gefunden.")
        End If

    End Sub


    ''' <summary>
    ''' Erstellt einen neuen Benutzer.
    ''' </summary>
    Sub CreateUser()

        If users.Length >= 999 Then
            Console.WriteLine("Maximale Anzahl von 999 Benutzern erreicht.")
        Else

            Console.Write("Neue Benutzer-ID: ")
            Dim newId As String = Console.ReadLine().Trim()

            Console.Write("Name: ")
            Dim newName As String = Console.ReadLine().Trim()

            Dim exists As Boolean = False

            For i As Integer = 0 To users.Length - 1
                If users(i).ID = newId Then
                    exists = True
                    Exit For
                End If
            Next

            If exists Then
                Console.WriteLine("Benutzer-ID existiert bereits.")
            Else

                ReDim Preserve users(users.Length)

                users(users.Length - 1).ID = newId
                users(users.Length - 1).Name = newName

                Console.WriteLine("Benutzer erfolgreich angelegt.")

            End If

        End If

    End Sub


    ''' <summary>
    ''' Zeigt alle ausgeliehenen Bücher eines bestimmten Benutzers an.
    ''' </summary>
    Sub ShowBorrowedBooksOfUser()

        Console.Write("Benutzer-ID eingeben: ")
        Dim userId As String = Console.ReadLine().Trim()

        Dim userFound As Boolean = False

        For i As Integer = 0 To users.Length - 1
            If users(i).ID = userId Then
                userFound = True
                Exit For
            End If
        Next

        If Not userFound Then
            Console.WriteLine("Benutzer nicht gefunden.")
            Return
        End If

        Console.WriteLine("Ausgeliehene Bücher:")

        For i As Integer = 0 To books.Length - 1

            If books(i).Status = "borrowed" Then
                Console.WriteLine(books(i).ISBN & " | " &
                                  books(i).Title & " | " &
                                  books(i).Author)
            End If

        Next

    End Sub

End Module