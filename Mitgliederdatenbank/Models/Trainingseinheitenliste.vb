Public Class Trainingseinheitenliste
    Private mlstTrainingseinheiten As List(Of Trainingseinheit)
    Public Sub New()
        mlstTrainingseinheiten = New List(Of Trainingseinheit)
    End Sub

    Public Sub New(pTrainingseinheiten As List(Of Trainingseinheit))
        mlstTrainingseinheiten = pTrainingseinheiten
    End Sub

    Public Property Trainingseinheiten() As List(Of Trainingseinheit)
        Get
            Return mlstTrainingseinheiten
        End Get
        Set(ByVal value As List(Of Trainingseinheit))
            mlstTrainingseinheiten = value
        End Set
    End Property

    Public Sub Hinzufuegen(pTrainingseinheit As Trainingseinheit)
        mlstTrainingseinheiten.Add(pTrainingseinheit)
    End Sub

    Public Sub Leeren()
        mlstTrainingseinheiten.Clear()
    End Sub
End Class
