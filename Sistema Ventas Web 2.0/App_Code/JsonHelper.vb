﻿Imports System.Runtime.Serialization.Json
Imports System.IO

Public Class JsonHelper
    ''' <summary>
    ''' JSON Serialization
    ''' </summary>
    Public Shared Function JsonSerializer(Of T)(ByVal a As T) As String
        Dim ser As DataContractJsonSerializer = New DataContractJsonSerializer(GetType(T))
        Dim ms As MemoryStream = New MemoryStream
        ser.WriteObject(ms, a)
        Dim jsonString As String = Encoding.UTF8.GetString(ms.ToArray)
        ms.Close()
        'Replace Json Date String
        Dim p As String = "\\/Date\((\d+)\+\d+\)\\/"
        Dim matchEvaluator As MatchEvaluator = New MatchEvaluator(AddressOf ConvertJsonDateToDateString)
        Dim reg As Regex = New Regex(p)
        jsonString = reg.Replace(jsonString, matchEvaluator)
        Return jsonString
    End Function

    ''' <summary>
    ''' JSON Deserialization
    ''' </summary>
    Public Shared Function JsonDeserialize(Of T)(ByVal jsonString As String) As T
        'Convert "yyyy-MM-dd HH:mm:ss" String as "\/Date(1319266795390+0800)\/"
        Dim p As String = "\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2}"
        Dim matchEvaluator As MatchEvaluator = New MatchEvaluator(AddressOf ConvertDateStringToJsonDate)
        Dim reg As Regex = New Regex(p)
        jsonString = reg.Replace(jsonString, matchEvaluator)
        Dim ser As DataContractJsonSerializer = New DataContractJsonSerializer(GetType(T))
        Dim ms As MemoryStream = New MemoryStream(Encoding.UTF8.GetBytes(jsonString))
        Dim obj As T = CType(ser.ReadObject(ms), T)
        Return obj
    End Function

    ''' <summary>
    ''' Convert Serialization Time /Date(1319266795390+0800) as String
    ''' </summary>
    Private Shared Function ConvertJsonDateToDateString(ByVal m As Match) As String
        Dim result As String = String.Empty
        Dim dt As DateTime = New DateTime(1970, 1, 1)
        dt = dt.AddMilliseconds(Long.Parse(m.Groups(1).Value))
        dt = dt.ToLocalTime
        result = dt.ToString("yyyy-MM-dd HH:mm:ss")
        Return result
    End Function

    ''' <summary>
    ''' Convert Date String as Json Time
    ''' </summary>
    Private Shared Function ConvertDateStringToJsonDate(ByVal m As Match) As String
        Dim result As String = String.Empty
        Dim dt As DateTime = DateTime.Parse(m.Groups(0).Value)
        dt = dt.ToUniversalTime
        Dim ts As TimeSpan = (dt - DateTime.Parse("1970-01-01"))
        result = String.Format("\/Date({0}+0800)\/", ts.TotalMilliseconds)
        Return result
    End Function

    ''Public Shared Function JsonSerializer(Of T)(ByVal a As T) As String
    ''    Dim ser As DataContractJsonSerializer = New DataContractJsonSerializer(GetType(T))
    ''    Dim ms As MemoryStream = New MemoryStream
    ''    ser.WriteObject(ms, a)
    ''    Dim jsonString As String = Encoding.UTF8.GetString(ms.ToArray)
    ''    ms.Close()
    ''    'Replace Json Date String
    ''    Dim p As String = "\\/Date\((\d+)\+\d+\)\\/"
    ''    Dim matchEvaluator As MatchEvaluator = New MatchEvaluator(ConvertJsonDateToDateString)
    ''    Dim reg As Regex = New Regex(p)
    ''    jsonString = reg.Replace(jsonString, matchEvaluator)
    ''    Return jsonString
    ''End Function


    ''Public Shared Function JsonDeserialize(Of T)(ByVal jsonString As String) As T
    ''    'Convert "yyyy-MM-dd HH:mm:ss" String as "\/Date(1319266795390+0800)\/"
    ''    Dim p As String = "\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2}"
    ''    Dim matchEvaluator As MatchEvaluator = New MatchEvaluator(ConvertDateStringToJsonDate)
    ''    Dim reg As Regex = New Regex(p)
    ''    jsonString = reg.Replace(jsonString, matchEvaluator)
    ''    Dim ser As DataContractJsonSerializer = New DataContractJsonSerializer(GetType(T))
    ''    Dim ms As MemoryStream = New MemoryStream(Encoding.UTF8.GetBytes(jsonString))
    ''    Dim obj As T = CType(ser.ReadObject(ms), T)
    ''    Return obj
    ''End Function

    ''Private Shared Function ConvertJsonDateToDateString(ByVal m As Match) As String
    ''    Dim result As String = String.Empty
    ''    Dim dt As DateTime = New DateTime(1970, 1, 1)
    ''    dt = dt.AddMilliseconds(Long.Parse(m.Groups(1).Value))
    ''    dt = dt.ToLocalTime
    ''    result = dt.ToString("yyyy-MM-dd HH:mm:ss")
    ''    Return result
    ''End Function

    ''Private Shared Function ConvertDateStringToJsonDate(ByVal m As Match) As String
    ''    Dim result As String = String.Empty
    ''    Dim dt As DateTime = DateTime.Parse(m.Groups(0).Value)
    ''    dt = dt.ToUniversalTime
    ''    Dim ts As TimeSpan = (dt - DateTime.Parse("1970-01-01"))
    ''    result = String.Format("\/Date({0}+0800)\/", ts.TotalMilliseconds)
    ''    Return result
    ''End Function

End Class
