Option Explicit
Dim goFS : Set goFS = CreateObject("Scripting.FileSystemObject")
Dim goVC : Set goVC = CreateObject("SAPI.SpVoice")
goVC.Speak goFS.OpenTextFile("inputThings.txt").ReadAll()