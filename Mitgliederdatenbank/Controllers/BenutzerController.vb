Public Class BenutzerController
    Inherits System.Web.Mvc.Controller                                   'Lädt die Frameworks

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
    ' GET: /Benutzer/Index

    Function Index() As ActionResult
        Dim benListe As BenutzerListe
        Dim ben As Benutzer

        benListe = New BenutzerListe

        For Each benEntity In db.tblBenutzer.ToList()
            ben = New Benutzer(benEntity)
            benListe.Hinzufuegen(ben)
        Next

        ' Sollte vorher ein Speichern erfolgt sein, das auf diese Seite zurückführt, 
        ' muss hier geprüft werden, ob ein Fehler aufgetreten ist
        If TempData.ContainsKey(CONCURRENCY_EXCEPTION) Then
            ' wenn ja wird eine Fehlermeldung zum ModelState hinzufügt und anschließend in der View angezeigt
            ModelState.AddModelError(String.Empty, TempData.Item(CONCURRENCY_EXCEPTION))
            ' Fehlermeldung aus temporärer Zwischenablage entfernen, um sie nicht noch einmal anzuzeigen
            TempData.Remove(CONCURRENCY_EXCEPTION)
        End If

        Return View(benListe)
    End Function

    ' Benutzer/Bearbeiten
    ' GET /Benutzer/Bearbeiten/1
    <HttpGet>
    Function Bearbeiten(ID As Integer) As ActionResult
        Dim ben As Benutzer
        Dim benEntity As BenutzerEntity = db.tblBenutzer.Find(ID)

        If IsNothing(benEntity) Then
            Return RedirectToAction("Index")
        End If

        db.Entry(benEntity).State = EntityState.Detached

        ben = New Benutzer(benEntity)
        Return View(ben)
    End Function

    <HttpPost>
    Function Bearbeiten(pben As Benutzer) As ActionResult
        Dim benEntity As BenutzerEntity

        benEntity = pben.gibAlsBenutzerEntity()

        If Not ModelState.IsValid Then
            Return View(pben)
        End If

        db.Entry(benEntity).State = EntityState.Modified


        db.SaveChanges()


        Return RedirectToAction("Index")

    End Function

    ' Neuen Benutzer hinzufügen
    ' GET /Benutzer/Hinzufuegen
    <HttpGet>
    Function Hinzufuegen() As ActionResult
        Return View()
    End Function

    <HttpPost>
    Function Hinzufuegen(pben As Benutzer) As ActionResult
        Dim benEntity As BenutzerEntity

        benEntity = pben.gibAlsBenutzerEntity()

        If ModelState.IsValid Then
            db.tblBenutzer.Attach(benEntity)
            db.Entry(benEntity).State = EntityState.Added
            db.SaveChanges()
            Return RedirectToAction("Index")
        End If

        Return View(pben)
    End Function

    ' Löschen
    ' GET /Benutzer/Loeschen/1

    Function Loeschen(ID As Integer) As ActionResult
        Dim ben As Benutzer
        Dim benEntity As BenutzerEntity = db.tblBenutzer.Find(ID)

        If IsNothing(benEntity) Then
            Return RedirectToAction("Index")
        End If

        db.Entry(benEntity).State = EntityState.Detached

        ben = New Benutzer(benEntity)
        Return View(ben)
    End Function

    <HttpPost()> _
    <ActionName("Loeschen")>
    Function LoeschenBestaetigt(pben As Benutzer) As ActionResult
        Dim benEntity As BenutzerEntity
        benEntity = pben.gibAlsBenutzerEntity()

        ' Beim Löschen sollte keine Prüfung des ModelState erfolgen, weil aus
        ' disabled-gesetzten Feldern der View keine Werte beim POST zurückgeliefert werden.
        ' In Abhängigkeit von den Pflichtattributen kann es also sein, dass der ModelState 
        ' nicht gültig wäre.

        'If Not ModelState.IsValid Then
        '    Return View(pben)
        'End If

        db.tblBenutzer.Attach(benEntity)
        db.Entry(benEntity).State = EntityState.Deleted

        Try
            db.SaveChanges()
        Catch ex As Exception
            ' Dann sollte der Index inkl. einer Fehlermeldung angezeigt werden, die über eine temporäre "Zwischenablage" zur
            ' anderen Controller-Methode transportiert wird
            TempData.Add(CONCURRENCY_EXCEPTION, "Löschen nicht möglich! Zwischenzeitlich ist die Aufgabe von " & _
                                     "einem anderen Benutzer geändert worden. Prüfen Sie die Änderungen, bevor die Löschen.")
        End Try

        Return RedirectToAction("Index")
    End Function

End Class