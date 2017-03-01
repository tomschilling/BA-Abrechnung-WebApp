Imports System.ComponentModel.DataAnnotations

'Am Beispiel des Mitglieds wird die Vorgehensweise und der Aufbau jeder verwendeten Model-Klasse in diesem Projekt gezeigt
'Die "Listen"-Model-Klassen werden an der MitgliederListe kommentiert, da sie im Aufbau alle identisch sind
Public Class Trainingseinheit

    Private mintIdPk As Integer
    Private mintMitIdFk As Integer
    Private mstrMitName As String
    Private mstrBenName As String

    Private mintBenIdFk As Integer
    Private mdatDatum As Date
    Private mbytVersion As Byte()

    Private _MitgliederEntity As MitgliedEntity
    Private _BenutzerEntity As BenutzerEntity

    Public Sub New(pTrainingseinheitenEntity As TrainingseinheitEntity)
        mintIdPk = pTrainingseinheitenEntity.trainIdPk
        mintMitIdFk = pTrainingseinheitenEntity.trainMitIdFk

        mintBenIdFk = pTrainingseinheitenEntity.trainBenIdFk

        mdatDatum = pTrainingseinheitenEntity.trainDatum
        mbytVersion = pTrainingseinheitenEntity.trainVersion
    End Sub

    ' TOF
    Public Sub New(pTrainingseinheitenEntity As TrainingseinheitEntity, pBenutzerEntity As BenutzerEntity, pMitgliederEntity As MitgliedEntity)
        mintIdPk = pTrainingseinheitenEntity.trainIdPk
        mintMitIdFk = pMitgliederEntity.mitIdPk
        mstrMitName = pMitgliederEntity.mitName
        mintBenIdFk = pBenutzerEntity.benIdPK
        mstrBenName = pBenutzerEntity.benName
        mdatDatum = pTrainingseinheitenEntity.trainDatum
        mbytVersion = pTrainingseinheitenEntity.trainVersion
    End Sub
    ' End TOF

    Public Sub New()
        mintIdPk = Nothing
        mintMitIdFk = Nothing

        mintBenIdFk = Nothing

        mdatDatum = Date.Now
        mbytVersion = Nothing
    End Sub

    Public Sub New(pintIdPk As Integer, pintMitIdFk As Integer, pstrMitName As String, pintBenIdFk As Integer, pstrBenName As String, pdatDatum As Date, pstrVersion As String)
        mintIdPk = pintIdPk
        mintMitIdFk = pintMitIdFk

        mintBenIdFk = pintBenIdFk

        mdatDatum = pdatDatum
        mbytVersion = Convert.FromBase64String(pstrVersion)
    End Sub

    Public Property IdPk() As Integer
        Get
            Return mintIdPk
        End Get
        Set(value As Integer)
            mintIdPk = value
        End Set
    End Property


    Public Property MitIdFk() As Integer
        Get
            Return mintMitIdFk
        End Get
        Set(value As Integer)
            mintMitIdFk = value
        End Set
    End Property


    Public Property MitName As String
        Get
            Return mstrMitName
        End Get
        Set(value As String)
            mstrMitName = value
        End Set
    End Property
    ' End TOF

    Public Property BenIdFk() As Integer
        Get
            Return mintBenIdFk
        End Get
        Set(value As Integer)
            mintBenIdFk = value
        End Set
    End Property


    Public Property BenName As String
        Get
            Return mstrBenName
        End Get
        Set(value As String)
            mstrBenName = value
        End Set
    End Property
    ' End TOF

    ' Bei der Version muss für die Darstellung in der View das Byte Array in Unicode umgewandelt werden
    Public Property Version As String
        Get
            If IsNothing(mbytVersion) Then
                Return Nothing
            Else
                Return Convert.ToBase64String(mbytVersion)
            End If
        End Get
        Set(value As String)
            mbytVersion = Convert.FromBase64String(value)
        End Set
    End Property



    Public Property Datum As Date
        Get
            Return mdatDatum
        End Get
        Set(value As Date)
            mdatDatum = value
        End Set
    End Property




    Public Function gibAlsTrainingseinheitenEntity() As TrainingseinheitEntity
        Dim trainE As TrainingseinheitEntity
        trainE = New TrainingseinheitEntity
        trainE.trainIdPk = mintIdPk
        trainE.trainMitIdFk = mintMitIdFk

        trainE.trainBenIdFk = mintBenIdFk

        trainE.trainDatum = mdatDatum
        trainE.trainVersion = mbytVersion



        Return trainE
    End Function

End Class


      
