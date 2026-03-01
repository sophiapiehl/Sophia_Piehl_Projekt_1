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
            Console.WriteLine("3 - Buch ausleihen")
            Console.WriteLine("4 - Buch zurückgeben")
            Console.WriteLine("5 - Benutzer anlegen")
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

            ElseIf input = "3" Then
                Console.Write("Benutzer-ID eingeben: ")
                Dim userId As String = Console.ReadLine().Trim()

                Console.Write("ISBN eingeben: ")
                Dim isbn As String = Console.ReadLine().Trim()

                ' Ausleihe prüfen
                Dim userFound As Boolean = False
                Dim bookFound As Boolean = False

                For i As Integer = 0 To users.Length - 1
                    If users(i).ID = userId Then
                        userFound = True
                        Exit For
                    End If
                Next

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

                If Not userFound Then
                    Console.WriteLine("Benutzer nicht gefunden.")
                End If

                If Not bookFound Then
                    Console.WriteLine("Buch nicht gefunden.")
                End If

            ElseIf input = "4" Then

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

            ElseIf input = "5" Then

                If users.Length >= 999 Then
                    Console.WriteLine("Maximale Anzahl von 999 Benutzern erreicht.")
                Else

                    Console.Write("Neue Benutzer-ID: ")
                    Dim newId As String = Console.ReadLine().Trim()

                    Console.Write("Name: ")
                    Dim newName As String = Console.ReadLine().Trim()

                    ' Prüfen ob ID bereits existiert
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
                        ' Array vergrößern
                        ReDim Preserve users(users.Length)

                        users(users.Length - 1).ID = newId
                        users(users.Length - 1).Name = newName

                        Console.WriteLine("Benutzer erfolgreich angelegt.")
                    End If

                End If

            ElseIf input = "0" Then
                running = False

            Else
                Console.WriteLine("Ungültige Eingabe.")
            End If

            Console.WriteLine()

        End While

    End Sub
End Module