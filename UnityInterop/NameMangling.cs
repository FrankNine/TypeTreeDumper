namespace Unity;

using System;

public static class NameMangling
{
    public static string Ptr64 
        => Environment.Is64BitProcess ? "E" : "";
}