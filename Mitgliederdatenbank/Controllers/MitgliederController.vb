Imports iTextSharp.text                                             'Lädt die Frameworks
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html.simpleparser
Imports System
Imports System.IO
Imports System.Data
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Linq
Imports System.Collections.Generic

Public Class MitgliederController
    Inherits System.Web.Mvc.Controller


    Private db As New dbEntities

    Private Const CONCURRENCY_EXCEPTION As String = "DBUpdateConcurrencyException"

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        db.Dispose()
        MyBase.Dispose(disposing)
    End Sub

    Function Test() As ActionResult
        Return View()
    End Function

    '
    ' GET: /Mitglieder

    Function Index() As ActionResult
        Dim mitListe As MitgliederListe
        Dim mit As Mitglied

        mitListe = New MitgliederListe

        For Each mitEntity In db.tblMitglieder.ToList()                 'Alle Mitglieder in mitListe laden
            mit = New Mitglied(mitEntity)
            mitListe.Hinzufuegen(mit)
        Next

        ' Sollte vorher ein Speichern erfolgt sein, das auf diese Seite zurückführt, 
        ' muss hier geprüft werden, ob ein Fehler aufgetreten ist
        If TempData.ContainsKey(CONCURRENCY_EXCEPTION) Then
            ' wenn ja wird eine Fehlermeldung zum ModelState hinzufügt und anschließend in der View angezeigt
            ModelState.AddModelError(String.Empty, TempData.Item(CONCURRENCY_EXCEPTION))
            ' Fehlermeldung aus temporärer Zwischenablage entfernen, um sie nicht noch einmal anzuzeigen
            TempData.Remove(CONCURRENCY_EXCEPTION)
        End If

        Return View(mitListe)                                           'Die Liste der Mitglieder wird an die Mitglieder übergeben
    End Function

    ' Mitglieder/Bearbeiten
    ' GET /Mitglieder/Bearbeiten/1
    <HttpGet>
    Function Bearbeiten(ID As Integer) As ActionResult
        Dim mit As Mitglied
        Dim mitEntity As MitgliedEntity = db.tblMitglieder.Find(ID)     'Mitglied mit der ID laden

        If IsNothing(mitEntity) Then                                    'Gibt es das Mitglied nicht dann zurück zur IndexView
            Return RedirectToAction("Index")
        End If

        db.Entry(mitEntity).State = EntityState.Detached

        mit = New Mitglied(mitEntity)
        Return View(mit)                                                'Informationen zum Mitglied werden an die View übergeben
    End Function

    <HttpPost>
    Function Bearbeiten(pmit As Mitglied) As ActionResult               'Das bearbeitete Mitglied wird von der View Mitlgieder/Bearbeiten/1 
        Dim mitEntity As MitgliedEntity                                 'an den MitgliederController gegeben

        mitEntity = pmit.gibAlsMitgliederEntity()

        If Not ModelState.IsValid Then                                  'Wenn die Änderungen nicht gültig sind, gib sie der View zurück 
            Return View(pmit)
        End If

        db.Entry(mitEntity).State = EntityState.Modified                'Sonst übernehme die Änderungen

        Try
            db.SaveChanges()                                            'Versuche zu speichern

        Catch ex As Exception

            ' Wenn ein Fehler auftritt, geht es hier weiter

            ' Deklaration der neuer Objekte, um nicht versehentlich alte Entitäten aus Cache zu laden
            Dim dbNeu As New dbEntities
            Dim mitEntityNeu As MitgliedEntity
            Dim mitNeu As Mitglied

            ' Neue Daten aus der Datenbank laden
            dbNeu = New dbEntities
            mitEntityNeu = dbNeu.tblMitglieder.Find(pmit.IdPk)

            ' Wenn der Datensatz in der Datenbank nicht mehr enthalten ist, wurde er zwischenzeitlich gelöscht
            If IsNothing(mitEntityNeu) Then
                ' Dann sollte der Index inkl. einer Fehlermeldung angezeigt werden, die über eine temporäre "Zwischenablage" zur
                ' anderen Controller-Methode transportiert wird
                TempData.Add(CONCURRENCY_EXCEPTION, "Speichern nicht möglich! Zwischenzeitlich ist das Mitglied von " & _
                                         "einem anderen Benutzer gelöscht worden.")
                ' Entity wurde gelöscht, eine Änderung ist nicht mehr sinnvoll
                Return RedirectToAction("Index")
            End If

            ' ModelState löschen, um wirklich neue Daten anzuzeigen
            ModelState.Clear()

            ' Neues Model aus Entity erzeugen, um es die zwischenzeitlich geänderten Daten in View anzuzeigen
            mitNeu = New Mitglied(mitEntityNeu)

            ' Beim Model den aufgetretenen Fehler vermerken
            ModelState.AddModelError(String.Empty, "Speichern nicht möglich! Zwischenzeitlich sind die Daten von " & _
                                     "einem anderen Benutzer geändert worden. Prüfen Sie die neu geladenen Daten.")

            ' Für jede Eigenschaft des Models, die sich geändert hat, als fehlerhaft vermerken
            If Not (mitNeu.Name = pmit.Name) Then
                ModelState.AddModelError("Name", "Ihre Eingabe war '" & pmit.Name & "'. Der neue Wert ist '" & mitNeu.Name & "'.")
            End If
            If Not (mitNeu.Vorname = pmit.Vorname) Then
                ModelState.AddModelError("Vorname", "Ihre Eingabe war '" & pmit.Vorname & "'. Der neue Wert ist '" & mitNeu.Vorname & "'.")
            End If
            If Not (mitNeu.Email = pmit.Email) Then
                ModelState.AddModelError("Email", "Ihre Eingabe war '" & pmit.Email & "'. Der neue Wert ist '" & mitNeu.Email & "'.")
            End If
            If Not (mitNeu.GebDatum = pmit.GebDatum) Then
                ModelState.AddModelError("GebDatum", "Ihre Eingabe war '" & pmit.GebDatum & "'. Der neue Wert ist '" & mitNeu.GebDatum & "'.")
            End If
            If Not (mitNeu.MtglDatum = pmit.MtglDatum) Then
                ModelState.AddModelError("MtglDatum", "Ihre Eingabe war '" & pmit.MtglDatum & "'. Der neue Wert ist '" & mitNeu.MtglDatum & "'.")
            End If
            If Not (mitNeu.PlzOrt = pmit.PlzOrt) Then
                ModelState.AddModelError("PlzOrt", "Ihre Eingabe war '" & pmit.PlzOrt & "'. Der neue Wert ist '" & mitNeu.PlzOrt & "'.")
            End If
            If Not (mitNeu.StraßeNr = pmit.StraßeNr) Then
                ModelState.AddModelError("StraßeNr", "Ihre Eingabe war '" & pmit.StraßeNr & "'. Der neue Wert ist '" & mitNeu.StraßeNr & "'.")
            End If
            If Not (mitNeu.Tel = pmit.Tel) Then
                ModelState.AddModelError("Tel", "Ihre Eingabe war '" & pmit.Tel & "'. Der neue Wert ist '" & mitNeu.Tel & "'.")
            End If


            ' neu geladenes Model und Fehler in View anzeigen
            Return View(mitNeu)

        End Try

        Return RedirectToAction("Index")                                'Kehre zur Index Function des Controllers zurück  

    End Function

    ' Neues Mitglied hinzufügen
    ' GET /Mitglieder/Hinzufuegen
    <HttpGet>
    Function Hinzufuegen() As ActionResult
        Return View()
    End Function

    <HttpPost>
    Function Hinzufuegen(pmit As Mitglied) As ActionResult
        Dim mitEntity As MitgliedEntity

        mitEntity = pmit.gibAlsMitgliederEntity()

        If ModelState.IsValid Then
            db.tblMitglieder.Attach(mitEntity)
            db.Entry(mitEntity).State = EntityState.Added                   'hinzufügen einer neuen Mitglieder Entität
            db.SaveChanges()
            Return RedirectToAction("Index")
        End If

        Return View(pmit)
    End Function

    ' Löschen
    ' GET /Mitglieder/Loeschen/1

    Function Loeschen(ID As Integer) As ActionResult
        Dim mit As Mitglied
        Dim mitEntity As MitgliedEntity = db.tblMitglieder.Find(ID)

        If IsNothing(mitEntity) Then
            Return RedirectToAction("Index")
        End If

        db.Entry(mitEntity).State = EntityState.Detached

        mit = New Mitglied(mitEntity)
        Return View(mit)
    End Function

    <HttpPost()> _
    <ActionName("Loeschen")>
    Function LoeschenBestaetigt(pmit As Mitglied) As ActionResult
        Dim mitEntity As MitgliedEntity
        mitEntity = pmit.gibAlsMitgliederEntity()

        ' Beim Löschen sollte keine Prüfung des ModelState erfolgen, weil aus
        ' disabled-gesetzten Feldern der View keine Werte beim POST zurückgeliefert werden.
        ' In Abhängigkeit von den Pflichtattributen kann es also sein, dass der ModelState 
        ' nicht gültig wäre.

        'If Not ModelState.IsValid Then
        '    Return View(pmit)
        'End If

        db.tblMitglieder.Attach(mitEntity)
        db.Entry(mitEntity).State = EntityState.Deleted

        Try
            db.SaveChanges()
        Catch ex As Exception
            ' Dann sollte der Index inkl. einer Fehlermeldung angezeigt werden, die über eine temporäre "Zwischenablage" zur
            ' anderen Controller-Methode transportiert wird
            TempData.Add(CONCURRENCY_EXCEPTION, "Löschen nicht möglich! Zwischenzeitlich ist das Mitglied von " & _
                                     "einem anderen Benutzer geändert worden. Prüfen Sie die Änderungen, bevor die Löschen.")
        End Try

        Return RedirectToAction("Index")
    End Function

    <HttpGet>
    Public Function TrainingseinheitenEinsehen(ID As Integer) As ActionResult           'Funktion zum Einsehen der Trainingseinheiten eines ausgewählten Mitglieds

        Dim mit As Mitglied
        Dim mitEntity As MitgliedEntity = db.tblMitglieder.Find(ID)

        Dim mitglEntity As MitgliedEntity
        Dim benEntity As BenutzerEntity

        If IsNothing(mitEntity) Then
            Return RedirectToAction("Index")
        End If

        db.Entry(mitEntity).State = EntityState.Detached

        mit = New Mitglied(mitEntity)

        Dim trainListe As Trainingseinheitenliste
        Dim train As Trainingseinheit
        trainListe = New Trainingseinheitenliste

        For Each trainEntity In db.tblTrainingseinheiten.ToList()

            mitglEntity = trainEntity.tblMitglieder
            benEntity = trainEntity.tblBenutzer

            train = New Trainingseinheit(trainEntity, benEntity, mitglEntity)

            If train.MitIdFk = ID Then                                                      'wenn der Fremdschlüssel der gefundenen Trainingseinheiten = mit der ausgewählten MitgliedsID ist füge es der Liste von Trainingseinheiten hinzu
                trainListe.Hinzufuegen(train)
            End If
        Next



        Return View(trainListe)                                                             'übergebe die Liste von Trainingseinheiten

    End Function


    <HttpPost>
    Public Function TrainingeinheitenEinsehen(ptrainListe As Trainingseinheitenliste) As ActionResult

        Return (View())

    End Function

    Public Function AbrechnungErstellen(ID As Integer) As ActionResult      'vgl. „iText Developers”,http://developers.itextpdf.com/question/how-convert-html-pdf, 18.03.2016

        Dim mit As Mitglied
        Dim mitEntity As MitgliedEntity = db.tblMitglieder.Find(ID)         'finde das vom Benutzer ausgewählte Mitglied anhand der ID

        Dim mitglEntity As MitgliedEntity
        Dim benEntity As BenutzerEntity

        If IsNothing(mitEntity) Then                                        'wenn es die ID nicht gibt kehre zurück zur Index
            Return RedirectToAction("Index")
        End If

        db.Entry(mitEntity).State = EntityState.Detached

        mit = New Mitglied(mitEntity)                                       'mit ist neu vom Typ Mitglied, gefüllt aus der MitgliedEntity

        Dim trainListe As Trainingseinheitenliste
        Dim train As Trainingseinheit
        trainListe = New Trainingseinheitenliste

        For Each trainEntity In db.tblTrainingseinheiten.ToList()

            mitglEntity = trainEntity.tblMitglieder
            benEntity = trainEntity.tblBenutzer

            train = New Trainingseinheit(trainEntity, benEntity, mitglEntity)
            If train.MitIdFk = ID And train.Datum.Month = Now.Month - 1 Then
                trainListe.Hinzufuegen(train)
            End If
        Next

        If trainListe.Trainingseinheiten.Count > 0 Then

            Dim output As New MemoryStream                          'MemoryStream = Stream der Arbeitsspeicher als Hintergrundspeicher verwendet [1] dieser wird benötigt, um zu schreiben [2] 
            Dim pdfdoc As New iTextSharp.text.Document(PageSize.A4, 50, 50, 50, 50)     'Erzeugen eines iTextSharp Dokuments als Abstraktion eines PDF Dokuments und legt die größe fest. Ist nicht das wirkliche PDF dokument.
            Dim writer As PdfWriter                                                 'Erzeugen einer variable zum schreiben
            writer = PdfWriter.GetInstance(pdfdoc, output)                      'writer ist an den MemoryStream und an die PDF Abstraktion(iTextSharp-Dokument gebunden
            writer.CloseStream = False                 'Der Stream soll nach der Übertragung nicht geschlossen werden, damit auf ihn zugegriffen werden kann.

            pdfdoc.Open()                                                   'öffnet das Dokument zum schreiben 

            Dim title As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Arial", 18)
            Dim standard As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Arial", 10)
            Dim small As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.UNDERLINE)
            Dim standardbold As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Arial", 10, Font.BOLD)
            Dim smallitalic As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.ITALIC)
            Dim logo As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(AppDomain.CurrentDomain.BaseDirectory + "\\images\\Logo.png")
            Dim table As PdfPTable = New PdfPTable(4)
            Dim anzahltrain As Integer = trainListe.Trainingseinheiten.Count
            Dim AbrechMonat As Integer
            AbrechMonat = Now.Month - 1
            Dim Betrag As Single = anzahltrain * 70
            Dim Netto As Single
            Netto = Betrag - (19 / 119 * Betrag)
            Dim Ustg As Single
            Ustg = Betrag - Netto
            Dim cell As PdfPCell
            cell = New PdfPCell(New Phrase("Mitglied"))             'Tabellenkopf einfügen
            cell.Colspan = 1
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Trainer"))
            cell.Colspan = 1
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Datum"))
            cell.Colspan = 1
            table.AddCell(cell)
            cell = New PdfPCell(New Phrase("Preis"))
            cell.Colspan = 1
            table.AddCell(cell)

            For Each train In trainListe.Trainingseinheiten.ToList          'Tabelleninhalt füllen
                table.AddCell(train.MitName)
                table.AddCell(train.BenName)
                table.AddCell(train.Datum.ToShortDateString())
                table.AddCell("70,00 €")
            Next


            logo.ScalePercent(30.5F)
            logo.Alignment = iTextSharp.text.Image.ALIGN_TOP & iTextSharp.text.Image.ALIGN_MIDDLE


            pdfdoc.Add(logo)


            pdfdoc.Add(AddEmptyParagraph())
            pdfdoc.Add(AddEmptyParagraph())
            pdfdoc.Add(AddEmptyParagraph())

            pdfdoc.Add(CreateParagraph("                   Monatsabrechnung - " & MonthName(AbrechMonat, False) & " " & Date.Now.ToString("yyyy"), title))
            pdfdoc.Add(AddEmptyParagraph())
            pdfdoc.Add(AddEmptyParagraph())
            pdfdoc.Add(CreateParagraph("KÖRPERBAU, Hannes Böhme, Kurfürstenstr. 38, 10785 Berlin", small))
            pdfdoc.Add(AddEmptyParagraph())
            pdfdoc.Add(CreateParagraph(mit.Vorname & " " & mit.Name, standard))
            pdfdoc.Add(CreateParagraph(mit.StraßeNr, standard))
            pdfdoc.Add(CreateParagraph(mit.PlzOrt, standard))
            pdfdoc.Add(AddEmptyParagraph())
            'pdfdoc.Add(CreateParagraph("Mitglied seit: " & mit.MtglDatum.ToShortDateString(), standard))
            'pdfdoc.Add(CreateParagraph("Mitgliedsnummmer: " & mit.IdPk, standard))
            'pdfdoc.Add(CreateParagraph("Steuernummer: 32/234/00917", standard))
            'pdfdoc.Add(CreateParagraph("Rechnungsdatum: " + DateTime.Now.ToShortDateString(), standard))

            pdfdoc.Add(AddEmptyParagraph())
            pdfdoc.Add(CreateParagraph("Liebe(r) " & mit.Vorname & ",", standardbold))
            pdfdoc.Add(AddEmptyParagraph())
            pdfdoc.Add(CreateParagraph("herzlichen Glückwunsch. Du hast es geschafft, im " & MonthName(AbrechMonat, False) & ", eine Anzahl von " & anzahltrain & " Trainingseinheiten zu absolvieren. Bitte überweise deinen Rechnungsbetrag in Höhe von " & Format([Betrag], "#.0,0 €") & " , binnen 7 Werktagen auf das unten genannte Konto.", standard))
            pdfdoc.Add(AddEmptyParagraph())
            pdfdoc.Add(CreateParagraph("Vielen lieben Dank !", standardbold))
            pdfdoc.Add(AddEmptyParagraph())
            pdfdoc.Add(AddEmptyParagraph())
            pdfdoc.Add(CreateParagraph("Deine absolvierten Trainingseinheiten im " + MonthName(AbrechMonat, False) & ":", standardbold))
            pdfdoc.Add(AddEmptyParagraph())
            pdfdoc.Add(table)                   'Tabelle ausgeben
            pdfdoc.Add(AddEmptyParagraph())
            pdfdoc.Add(CreateParagraph("Durch deine Anzahl von absolvierten Trainingseinheiten, ergibt sich als Rechnungsbetrag", standardbold))
            pdfdoc.Add(CreateParagraph("eine Summe von " & Format([Betrag], "#.0,0 €") & " incl. 19% Umsatzsteuer (" & Format([Ustg], "#.0,0 €") & ").", standardbold))
            pdfdoc.Add(AddEmptyParagraph())
            ' pdfdoc.Add(CreateParagraph("Enthaltende Umsatzsteuer: " & Format([Ustg], "#.0,0 €") & ".", standardbold))
            pdfdoc.Add(AddEmptyParagraph())
            pdfdoc.Add(AddEmptyParagraph())
            pdfdoc.Add(CreateParagraph("Kontodaten", standardbold))
            pdfdoc.Add(AddEmptyParagraph())
            pdfdoc.Add(CreateParagraph("Kontoinhaber: Hannes Böhme", standard))
            pdfdoc.Add(CreateParagraph("IBAN: DE76 1009 0000 5605 4590 15", standard))
            pdfdoc.Add(CreateParagraph("BIC: EVODEBBXXX", standard))

            pdfdoc.Add(AddEmptyParagraph())
            pdfdoc.Add(AddEmptyParagraph())


            pdfdoc.Add(CreateParagraph("Mitglied seit: " & mit.MtglDatum.ToShortDateString() & " | Mitglieds-Nr.: " & mit.IdPk & " | Steuernummer: 32/234/00917 | Rechnungsdatum: " + DateTime.Now.ToShortDateString(), standard))
            pdfdoc.Close()                  'schließt das iTextSharp Dokument

            output.Position = 0             'die Position des Streams wird auf 0 gesetzt

            Dim fileName As String
            Dim actMonth As Date

            actMonth = Format(DateSerial(Year(Now()), Month(Now()) - 1, 1), "y")

            fileName = actMonth.ToString("MMMM") & "-Abrechnung" & mit.Name & actMonth.ToString("yyyy") & ".pdf"

            HttpContext.Response.AppendHeader("content-disposition", "inline; filename=" & fileName)

            Return New FileStreamResult(output, "application/pdf")  'sendet binären Inhalt als Antwort übergeben wird nun der MemoryStream in dem geschrieben wurde und der Inhaltstyp der als Antwort gesendet werden soll)


        Else

            Dim AbrechMonat As Integer
            AbrechMonat = Now.Month - 1

            MsgBox(mit.Vorname & " " & mit.Name & " hat keine Trainingseinheiten im " & MonthName(AbrechMonat, False) & " absolviert. " & vbNewLine & vbNewLine & "Sobald Trainingseinheiten absolviert wurden, können diese im darauffolgenden Monat abgerechnet werden.", MsgBoxStyle.Information)

            Return RedirectToAction("Index")

        End If
        Return RedirectToAction("Index")
    End Function

    Public Function CreateParagraph(text As String, font As Font) As Paragraph                  'Funktion für einen Paragraphen 

        Return New iTextSharp.text.Paragraph(New iTextSharp.text.Chunk(text, font))

    End Function

    Public Function AddEmptyParagraph() As Paragraph                                            ''Funktion für einen leeren Paragraphen 

        Dim standard As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Tahoma", 10)

        Return CreateParagraph(Environment.NewLine, standard)

    End Function

End Class