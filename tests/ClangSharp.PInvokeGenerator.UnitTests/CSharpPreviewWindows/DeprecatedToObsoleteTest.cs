// Copyright (c) .NET Foundation and Contributors. All Rights Reserved. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

using System.Threading.Tasks;
using NUnit.Framework;

namespace ClangSharp.UnitTests;

[Platform("win")]
public sealed class CSharpPreviewWindows_DeprecatedToObsoleteTest : DeprecatedToObsoleteTest
{
    protected override Task SimpleStructMembersImpl(string nativeType, string expectedManagedType)
    {
        var inputContents = $@"struct MyStruct
{{
    {nativeType} r;
    
    [[deprecated]]
    {nativeType} g;

    [[deprecated(""This is obsolete."")]]
    {nativeType} b;

    {nativeType} a;
}};
";

        var expectedOutputContents = $@"using System;

namespace ClangSharp.Test
{{
    public partial struct MyStruct
    {{
        public {expectedManagedType} r;

        [Obsolete]
        public {expectedManagedType} g;

        [Obsolete(""This is obsolete."")]
        public {expectedManagedType} b;

        public {expectedManagedType} a;
    }}
}}
";

        return ValidateGeneratedCSharpPreviewWindowsBindingsAsync(inputContents, expectedOutputContents);
    }

    protected override Task StructDeclImpl()
    {
        var inputContents = $@"struct MyStruct0
{{
    int r;
}};

struct [[deprecated]] MyStruct1
{{
    int r;
}};

struct [[deprecated(""This is obsolete."")]] MyStruct2
{{
    int r;
}};

struct MyStruct3
{{
    int r;
}};
";

        var expectedOutputContents = $@"using System;

namespace ClangSharp.Test
{{
    public partial struct MyStruct0
    {{
        public int r;
    }}

    [Obsolete]
    public partial struct MyStruct1
    {{
        public int r;
    }}

    [Obsolete(""This is obsolete."")]
    public partial struct MyStruct2
    {{
        public int r;
    }}

    public partial struct MyStruct3
    {{
        public int r;
    }}
}}
";

        return ValidateGeneratedCSharpPreviewWindowsBindingsAsync(inputContents, expectedOutputContents);
    }

    protected override Task SimpleTypedefStructMembersImpl(string nativeType, string expectedManagedType)
    {
        var inputContents = $@"typedef struct
{{
    {nativeType} r;

    [[deprecated]]
    {nativeType} g;

    [[deprecated(""This is obsolete."")]]
    {nativeType} b;

    {nativeType} a;
}} MyStruct;
";

        var expectedOutputContents = $@"using System;

namespace ClangSharp.Test
{{
    public partial struct MyStruct
    {{
        public {expectedManagedType} r;

        [Obsolete]
        public {expectedManagedType} g;

        [Obsolete(""This is obsolete."")]
        public {expectedManagedType} b;

        public {expectedManagedType} a;
    }}
}}
";
        return ValidateGeneratedCSharpPreviewWindowsBindingsAsync(inputContents, expectedOutputContents);
    }

    protected override Task TypedefStructDeclImpl()
    {
        var inputContents = $@"typedef struct
{{
    int r;
}} MyStruct0;

[[deprecated]] typedef struct
{{
    int r;
}} MyStruct1;

[[deprecated(""This is obsolete."")]] typedef struct
{{
    int r;
}} MyStruct2;

typedef struct
{{
    int r;
}} MyStruct3;
";

        var expectedOutputContents = $@"using System;

namespace ClangSharp.Test
{{
    public partial struct MyStruct0
    {{
        public int r;
    }}

    [Obsolete]
    public partial struct MyStruct1
    {{
        public int r;
    }}

    [Obsolete(""This is obsolete."")]
    public partial struct MyStruct2
    {{
        public int r;
    }}

    public partial struct MyStruct3
    {{
        public int r;
    }}
}}
";

        return ValidateGeneratedCSharpPreviewWindowsBindingsAsync(inputContents, expectedOutputContents);
    }

    protected override Task SimpleEnumMembersImpl()
    {
        var inputContents = $@"enum MyEnum : int
{{
    MyEnum_Value0,
    MyEnum_Value1 [[deprecated]],
    MyEnum_Value2 [[deprecated(""This is obsolete."")]],
    MyEnum_Value3,
}};
";

        var expectedOutputContents = @"using System;

namespace ClangSharp.Test
{
    public enum MyEnum
    {
        MyEnum_Value0,
        [Obsolete]
        MyEnum_Value1,
        [Obsolete(""This is obsolete."")]
        MyEnum_Value2,
        MyEnum_Value3,
    }
}
";

        return ValidateGeneratedCSharpPreviewWindowsBindingsAsync(inputContents, expectedOutputContents);
    }

    protected override Task EnumDeclImpl()
    {
        var inputContents = $@"enum MyEnum0 : int
{{
    MyEnum_Value0,
}};

enum [[deprecated]] MyEnum1 : int
{{
    MyEnum_Value1,
}};

enum [[deprecated(""This is obsolete."")]] MyEnum2 : int
{{
    MyEnum_Value2,
}};


enum MyEnum3 : int
{{
    MyEnum_Value3,
}};
";

        var expectedOutputContents = @"using System;

namespace ClangSharp.Test
{
    public enum MyEnum0
    {
        MyEnum_Value0,
    }

    [Obsolete]
    public enum MyEnum1
    {
        MyEnum_Value1,
    }

    [Obsolete(""This is obsolete."")]
    public enum MyEnum2
    {
        MyEnum_Value2,
    }

    public enum MyEnum3
    {
        MyEnum_Value3,
    }
}
";

        return ValidateGeneratedCSharpPreviewWindowsBindingsAsync(inputContents, expectedOutputContents);
    }

    protected override Task SimpleVarDeclImpl(string nativeType, string expectedManagedType)
    {
        var inputContents = $@"
{nativeType} MyVariable0 = 0;

[[deprecated]]
{nativeType} MyVariable1 = 0;

[[deprecated(""This is obsolete."")]]
{nativeType} MyVariable2 = 0;

{nativeType} MyVariable3 = 0;";

        var expectedOutputContents = $@"using System;

namespace ClangSharp.Test
{{
    public static partial class Methods
    {{
        public static {expectedManagedType} MyVariable0 = 0;

        [Obsolete]
        public static {expectedManagedType} MyVariable1 = 0;

        [Obsolete(""This is obsolete."")]
        public static {expectedManagedType} MyVariable2 = 0;

        public static {expectedManagedType} MyVariable3 = 0;
    }}
}}
";

        return ValidateGeneratedCSharpPreviewWindowsBindingsAsync(inputContents, expectedOutputContents);
    }

    protected override Task FuncDeclImpl()
    {
        var inputContents = @"
void MyFunction0()
{
}

[[deprecated]]
void MyFunction1()
{
}

[[deprecated(""This is obsolete."")]]
void MyFunction2()
{
}

void MyFunction3()
{
}
";

        var expectedOutputContents = @"using System;

namespace ClangSharp.Test
{
    public static partial class Methods
    {
        public static void MyFunction0()
        {
        }

        [Obsolete]
        public static void MyFunction1()
        {
        }

        [Obsolete(""This is obsolete."")]
        public static void MyFunction2()
        {
        }

        public static void MyFunction3()
        {
        }
    }
}
";

        return ValidateGeneratedCSharpPreviewWindowsBindingsAsync(inputContents, expectedOutputContents);
    }

    protected override Task InstanceFuncImpl()
    {
        var inputContents = @"struct MyStruct
{
    int MyFunction0() { return 0; }

    [[deprecated]]
    int MyFunction1() { return 0; }

    [[deprecated(""This is obsolete."")]]
    int MyFunction2() { return 0; }

    int MyFunction3() { return 0; }
};";

        var expectedOutputContents = $@"using System;

namespace ClangSharp.Test
{{
    public partial struct MyStruct
    {{
        public int MyFunction0()
        {{
            return 0;
        }}

        [Obsolete]
        public int MyFunction1()
        {{
            return 0;
        }}

        [Obsolete(""This is obsolete."")]
        public int MyFunction2()
        {{
            return 0;
        }}

        public int MyFunction3()
        {{
            return 0;
        }}
    }}
}}
";

        return ValidateGeneratedCSharpPreviewWindowsBindingsAsync(inputContents, expectedOutputContents);
    }

    protected override Task FuncPtrDeclImpl()
    {
        var inputContents = @"
typedef void (*Callback0)();
[[deprecated]] typedef void (*Callback1)();
[[deprecated(""This is obsolete."")]] typedef void (*Callback2)();
typedef void (*Callback3)();

struct MyStruct0 {
    Callback0 _callback;
};
struct MyStruct1 {
    Callback1 _callback;
};
struct MyStruct2 {
    Callback2 _callback;
};
struct MyStruct3 {
    Callback3 _callback;
};
";

        var expectedOutputContents = @"using System;

namespace ClangSharp.Test
{
    public unsafe partial struct MyStruct0
    {
        [NativeTypeName(""Callback0"")]
        public delegate* unmanaged[Cdecl]<void> _callback;
    }

    public unsafe partial struct MyStruct1
    {
        [NativeTypeName(""Callback1"")]
        [Obsolete]
        public delegate* unmanaged[Cdecl]<void> _callback;
    }

    public unsafe partial struct MyStruct2
    {
        [NativeTypeName(""Callback2"")]
        [Obsolete(""This is obsolete."")]
        public delegate* unmanaged[Cdecl]<void> _callback;
    }

    public unsafe partial struct MyStruct3
    {
        [NativeTypeName(""Callback3"")]
        public delegate* unmanaged[Cdecl]<void> _callback;
    }
}
";

        return ValidateGeneratedCSharpPreviewWindowsBindingsAsync(inputContents, expectedOutputContents);
    }

    protected override Task FuncDllImportImpl()
    {
        var inputContents = @"
extern ""C"" void MyFunction0();
extern ""C"" [[deprecated]] void MyFunction1();
extern ""C"" [[deprecated(""This is obsolete."")]] void MyFunction2();
extern ""C"" void MyFunction3();";

        var expectedOutputContents = @"using System;
using System.Runtime.InteropServices;

namespace ClangSharp.Test
{
    public static partial class Methods
    {
        [DllImport(""ClangSharpPInvokeGenerator"", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void MyFunction0();

        [DllImport(""ClangSharpPInvokeGenerator"", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [Obsolete]
        public static extern void MyFunction1();

        [DllImport(""ClangSharpPInvokeGenerator"", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [Obsolete(""This is obsolete."")]
        public static extern void MyFunction2();

        [DllImport(""ClangSharpPInvokeGenerator"", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void MyFunction3();
    }
}
";

        return ValidateGeneratedCSharpPreviewWindowsBindingsAsync(inputContents, expectedOutputContents);
    }
}
