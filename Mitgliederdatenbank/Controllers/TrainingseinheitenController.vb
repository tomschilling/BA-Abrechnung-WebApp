

Public Class TrainingseinheitenController
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
    ' GET: /Trainingseinheiten

    Function Index() As ActionResult
        Dim trainListe As Trainingseinheitenliste
        Dim train As Trainingseinheit


        Dim mitglEntity As MitgliedEntity
        Dim benEntity As BenutzerEntity


        trainListe = New Trainingseinheitenliste

        For Each trainEntity In db.tblTrainingseinheiten.ToList()


            mitglEntity = trainEntity.tblMitglieder
            benEntity = trainEntity.tblBenutzer


            train = New Trainingseinheit(trainEntity, benEntity, mitglEntity)
            trainListe.Hinzufuegen(train)
        Next


        ' Sollte vorher ein Speichern erfolgt sein, das auf diese Seite zurückführt, 
        ' muss hier geprüft werden, ob ein Fehler aufgetreten ist
        If TempData.ContainsKey(CONCURRENCY_EXCEPTION) Then
            ' wenn ja wird eine Fehlermeldung zum ModelState hinzufügt und anschließend in der View angezeigt
            ModelState.AddModelError(String.Empty, TempData.Item(CONCURRENCY_EXCEPTION))
            ' Fehlermeldung aus temporärer Zwischenablage entfernen, um sie nicht noch einmal anzuzeigen
            TempData.Remove(CONCURRENCY_EXCEPTION)
        End If

        Return View(trainListe)
    End Function

    ' Trainingseinheiten/Bearbeiten
    ' GET /Trainingseinheiten/Bearbeiten/1
    <HttpGet>
    Function Bearbeiten(ID As Integer) As ActionResult
        Dim train As Trainingseinheit
        Dim trainEntity As TrainingseinheitEntity = db.tblTrainingseinheiten.Find(ID)

        If IsNothing(trainEntity) Then
            Return RedirectToAction("Index")
        End If

        db.Entry(trainEntity).State = EntityState.Detached

        train = New Trainingseinheit(trainEntity)


        MtglDropDownList(train.MitIdFk)
        BenDropDownList(train.BenIdFk)


        Return View(train)
    End Function

    <HttpPost>
    Function Bearbeiten(ptrain As Trainingseinheit) As ActionResult
        Dim trainEntity As TrainingseinheitEntity

        trainEntity = ptrain.gibAlsTrainingseinheitenEntity()

        If Not ModelState.IsValid Then
            Return View(ptrain)
        End If

        db.Entry(trainEntity).State = EntityState.Modified


        db.SaveChanges()

        MtglDropDownList(ptrain.MitIdFk)
        BenDropDownList(ptrain.BenIdFk)


        Return RedirectToAction("Index")

    End Function

    ' Neue Trainingseinheit hinzufügen
    ' GET /Trainingseinheit/Hinzufuegen
    <HttpGet>
    Function Hinzufuegen() As ActionResult
        
        MtglDropDownList()
        BenDropDownList()

        Return View()

    End Function

    <HttpPost>
    Function Hinzufuegen(ptrain As Trainingseinheit) As ActionResult
        Dim trainEntity As TrainingseinheitEntity

        trainEntity = ptrain.gibAlsTrainingseinheitenEntity()


        If ModelState.IsValid Then
            db.tblTrainingseinheiten.Attach(trainEntity)
            db.Entry(trainEntity).State = EntityState.Added
            db.SaveChanges()
            Return RedirectToAction("Index")
        End If

        MtglDropDownList(ptrain.MitIdFk)
        BenDropDownList(ptrain.BenIdFk)
        Return View(ptrain)
    End Function

    ' Löschen
    ' GET /Trainingseinheiten/Loeschen/1

    Function Loeschen(ID As Integer) As ActionResult
        Dim train As Trainingseinheit
        Dim trainEntity As TrainingseinheitEntity = db.tblTrainingseinheiten.Find(ID)

        If IsNothing(trainEntity) Then
            Return RedirectToAction("Index")
        End If

        db.Entry(trainEntity).State = EntityState.Detached

        train = New Trainingseinheit(trainEntity)
        Return View(train)
    End Function

    <HttpPost()> _
    <ActionName("Loeschen")>
    Function LoeschenBestaetigt(ptrain As Trainingseinheit) As ActionResult
        Dim trainEntity As TrainingseinheitEntity
        trainEntity = ptrain.gibAlsTrainingseinheitenEntity()

        ' Beim Löschen sollte keine Prüfung des ModelState erfolgen, weil aus
        ' disabled-gesetzten Feldern der View keine Werte beim POST zurückgeliefert werden.
        ' In Abhängigkeit von den Pflichtattributen kann es also sein, dass der ModelState 
        ' nicht gültig wäre.

        'If Not ModelState.IsValid Then
        '    Return View(ptrain)
        'End If

        db.tblTrainingseinheiten.Attach(trainEntity)
        db.Entry(trainEntity).State = EntityState.Deleted

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

    Private Sub MtglDropDownList(Optional ByVal selectedMtgl As Object = Nothing)           'Sub zum füllen des ViewBags für DropDownList Mitglieder in View
        Dim MtglQuery = db.tblMitglieder.OrderBy(Function(mit) mit.mitName)
        ViewBag.mitIdFk = New SelectList(MtglQuery, "mitIdPk", "mitName", selectedMtgl)
    End Sub

    Private Sub BenDropDownList(Optional ByVal selectedBen As Object = Nothing)             'Sub zum füllen des ViewBags für DropDownList Benutzer in View
        Dim BenQuery = db.tblBenutzer.OrderBy(Function(ben) ben.benName)
        ViewBag.benIdFk = New SelectList(BenQuery, "benIdPk", "benName", selectedBen)
    End Sub
End Class