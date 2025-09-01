namespace Unity;

using System;
using System.Text.RegularExpressions;

public enum UnityVersionType
{
    Alpha,
    Beta,
    Final,
    Patch,
    Experimental
}

public readonly struct UnityVersion : IComparable<UnityVersion>, IEquatable<UnityVersion>
{
    public readonly int Major;
    public readonly int Minor;
    public readonly int Patch;
    public readonly UnityVersionType Type;
    public readonly int Build;

    public static readonly UnityVersion Unity3_5 = new(3, 5);
    
    public static readonly UnityVersion Unity4_0 = new(4, 0);

    public static readonly UnityVersion Unity5_0 = new(5, 0);
    public static readonly UnityVersion Unity5_2 = new(5, 2);
    public static readonly UnityVersion Unity5_3 = new(5, 3);
    public static readonly UnityVersion Unity5_4 = new(5, 4);
    public static readonly UnityVersion Unity5_5 = new(5, 5);

    public static readonly UnityVersion Unity2017_1 = new(2017, 1);
    public static readonly UnityVersion Unity2017_2 = new(2017, 2);
    public static readonly UnityVersion Unity2017_3 = new(2017, 3);

    public static readonly UnityVersion Unity2019_1 = new(2019, 1);
    public static readonly UnityVersion Unity2019_3 = new(2019, 3);

    public static readonly UnityVersion Unity2022_2 = new(2022, 2);

    public static readonly UnityVersion Unity2023_1_0a2 = new(2023, 1, 0, 'a', 2);

    public static readonly UnityVersion Unity6000_2 = new(6000, 2);

    public UnityVersion(int major, int minor)
        : this(major, minor, 0, 0, 0)
    {
    }

    public UnityVersion(int major, int minor, int patch)
        : this(major, minor, patch, 0, 0)
    {
    }

    public UnityVersion(int major, int minor, int patch, char type, int build)
        : this(major, minor, patch, VersionTypeFromChar(type), build)
    {
    }

    public UnityVersion(int major, int minor, int patch, UnityVersionType type, int build)
    {
        Major = major;
        Minor = minor;
        Patch = patch;
        Type  = type;
        Build = build;
    }

    public UnityVersion(string version)
    {
        var match = Regex.Match(version, @"(\d+)\.(\d+)\.(\d+)([abfpx])(\d+)");

        if (!match.Success)
            throw new ArgumentException("Invalid version string.", nameof(version));

        Major = int.Parse(match.Groups[1].Value);
        Minor = int.Parse(match.Groups[2].Value);
        Patch = int.Parse(match.Groups[3].Value);
        Type  = VersionTypeFromChar(match.Groups[4].Value[0]);
        Build = int.Parse(match.Groups[5].Value);
    }

    public int CompareTo(UnityVersion other)
    {
        if (Major != other.Major)
            return Major.CompareTo(other.Major);

        if (Minor != other.Minor)
            return Minor.CompareTo(other.Minor);

        if (Patch != other.Patch)
            return Patch.CompareTo(other.Patch);

        if (Type != other.Type)
            return Type.CompareTo(other.Type);

        return Build.CompareTo(other.Build);
    }

    public static bool operator ==(UnityVersion a, UnityVersion b) => a.CompareTo(b) == 0;
    public static bool operator !=(UnityVersion a, UnityVersion b) => a.CompareTo(b) != 0;
    public static bool operator >(UnityVersion a, UnityVersion b) => a.CompareTo(b) > 0;
    public static bool operator <(UnityVersion a, UnityVersion b) => a.CompareTo(b) < 0;
    public static bool operator >=(UnityVersion a, UnityVersion b) => a.CompareTo(b) >= 0;
    public static bool operator <=(UnityVersion a, UnityVersion b) => a.CompareTo(b) <= 0;

    public static explicit operator Version(UnityVersion version) => new Version(version.Major,version.Minor,version.Patch,version.Build);

    public bool Equals(UnityVersion other) => CompareTo(other) == 0;
    public override bool Equals(object obj) => obj is UnityVersion version && Equals(version);

    public override int GetHashCode() => HashCode.Combine(Major, Minor, Patch, Type, Build);

    public override string ToString() => $"{Major}.{Minor}.{Patch}{CharFromVersionType(Type)}{Build}";

    static char CharFromVersionType(UnityVersionType type) => type switch
    {
        UnityVersionType.Alpha        => 'a',
        UnityVersionType.Beta         => 'b',
        UnityVersionType.Final        => 'f',
        UnityVersionType.Patch        => 'p',
        UnityVersionType.Experimental => 'x',
        _                             => throw new ArgumentOutOfRangeException(nameof(type))
    };

    static UnityVersionType VersionTypeFromChar(char c) => c switch
    {
        'a' => UnityVersionType.Alpha,
        'b' => UnityVersionType.Beta,
        'f' => UnityVersionType.Final,
        'p' => UnityVersionType.Patch,
        'x' => UnityVersionType.Experimental,
        _   => throw new ArgumentOutOfRangeException(nameof(c))
    };
}