Imports System

Module Program
    Dim libraryTestData As String =
    "978-3-16-148410-0;Einführung in die Informatik;Müller;verfügbar|" &
    "978-0-13-110362-7;Programmieren mit VB.NET;Schneider;verfügbar|" &
    "978-3-540-69006-6;Grundlagen der Softwaretechnik;Meier;ausgeliehen|" &
    "978-3-642-05445-3;Datenstrukturen und Algorithmen;Klein;verfügbar"


    Dim usrTestData As String =
    "U001;Max Mustermann|" &
    "U002;Erika Musterfrau|" &
    "U003;Hans Meier|" &
    "U004;Laura Schmidt"

    ''' ----------------------------------------------------------
    ''' <summary>
    ''' Zeigt alle im System hinterlegten Benutzer an.
    ''' Die Benutzerdaten werden aus den Testdaten eingelesen
    ''' und formatiert auf der Konsole ausgegeben.
    ''' </summary>
    ''' <remarks>
    ''' Es werden die vorhandenen Testdaten verwendet.
    ''' </remarks>
    Sub ShowUsers()

        ' Testdaten in Array aufteilen
        Dim users() As String = usrTestData.Split("|"c)

        ' Schleife über alle Benutzer
        For i As Integer = 0 To users.Length - 1

            ' Benutzer in ID und Name aufteilen
            Dim details() As String = users(i).Split(";"c)

            Console.WriteLine("Benutzer-ID: " & details(0))
            Console.WriteLine("Name: " & details(1))
            Console.WriteLine("----------------------------")

        Next

    End Sub

    ''' ----------------------------------------------------------
    ''' <summary>
    ''' Hauptsteuerung des Programms.
    ''' Zeigt das Konsolenmenü an, verarbeitet die Benutzereingaben
    ''' und ruft je nach Auswahl die entsprechenden Funktionen auf.
    ''' </summary>
    ''' <remarks>
    ''' Das Menü läuft in einer While-Schleife,
    ''' bis der Benutzer das Programm beendet.
    ''' </remarks>
    Sub Main()

        Dim running As Boolean = True

        While running

            Console.WriteLine("===== Bibliothekssystem =====")
            Console.WriteLine("1 - Neuen Benutzer anlegen")
            Console.WriteLine("2 - Alle Bücher anzeigen")
            Console.WriteLine("3 - Alle Benutzer anzeigen")
            Console.WriteLine("4 - Buch ausleihen")
            Console.WriteLine("5 - Buch zurückgeben")
            Console.WriteLine("6 - Ausgeliehene Bücher anzeigen")
            Console.WriteLine("0 - Programm beenden")
            Console.Write("Bitte Auswahl eingeben: ")

            Dim input As String = Console.ReadLine().Trim()

            If input = "1" Then

                Console.Write("Bitte neue Benutzer-ID eingeben: ")

                Dim newID As String = Console.ReadLine().Trim()

                If newID = "" Then
                    Console.WriteLine("Benutzer-ID darf nicht leer sein!")

                Else
                    Console.Write("Bitte Namen eingeben: ")
                    Dim newName As String = Console.ReadLine().Trim()

                    If newName = "" Then
                        Console.WriteLine("Name darf nicht leer sein!")

                    Else
                        Console.WriteLine("Neuer Benutzer wurde angelegt:")
                        Console.WriteLine("ID: " & newID)
                        Console.WriteLine("Name: " & newName)
                    End If
                End If

            ElseIf input = "2" Then

                Dim books() As String = libraryTestData.Split("|"c)

                For i As Integer = 0 To books.Length - 1

                    Dim details() As String = books(i).Split(";"c)

                    Console.WriteLine("ISBN: " & details(0))
                    Console.WriteLine("Titel: " & details(1))
                    Console.WriteLine("Autor: " & details(2))
                    Console.WriteLine("Status: " & details(3))
                    Console.WriteLine("----------------------------")

                Next

            ElseIf input = "3" Then

                ShowUsers()

            ElseIf input = "4" Then
                Console.WriteLine("Buch ausleihen gewählt.")

            ElseIf input = "5" Then
                Console.WriteLine("Buch zurückgeben gewählt.")

            ElseIf input = "6" Then
                Console.WriteLine("Ausgeliehene Bücher anzeigen gewählt.")

            ElseIf input = "0" Then
                running = False

            Else
                Console.WriteLine("Ungültige Eingabe!")

            End If

            Console.WriteLine()

        End While

    End Sub

End Module
