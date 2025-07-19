# 🧩 .NET Multi-App Architecture: SharedLib + ConsoleApp + NativeApp + R2RApp

This solution demonstrates how to build and run three types of .NET applications — regular, Native AOT, and ReadyToRun — that share a common library (`SharedLib`).

## 📁 Project Structure

```

MySolution/
├── SharedLib/     # Common logic used by all apps
├── ConsoleApp/    # Standard .NET Console App (JIT runtime)
├── NativeApp/     # Ahead-of-Time compiled native app (no runtime needed)
├── R2RApp/        # ReadyToRun compiled app (runtime required, faster startup)
└── MySolution.sln # Solution file

````

---

## 📦 Shared Library: `SharedLib`

Contains reusable C# code like:

```csharp
public class MathUtils
{
    public static int Add(int a, int b) => a + b;
}
````

Used in all three app types.

---

## 🚀 Apps Overview & Comparison

| Feature                      | `ConsoleApp`         | `NativeApp` (AOT)    | `R2RApp` (ReadyToRun)  |
| ---------------------------- | -------------------- | -------------------- | ---------------------- |
| Uses JIT                     | ✅ Yes                | ❌ No                 | ✅ Yes (precompiled IL) |
| Uses AOT                     | ❌ No                 | ✅ Full AOT           | ✅ Partial (R2R)        |
| Needs .NET runtime installed | ✅ Yes                | ❌ No                 | ✅ Yes                  |
| Supports `Assembly.Load`     | ✅ Yes                | ❌ No                 | ✅ Yes                  |
| Supports full reflection     | ✅ Yes                | ⚠️ Limited           | ✅ Yes                  |
| Supports plugin loading      | ✅ Yes                | ❌ No                 | ✅ Yes                  |
| Startup performance          | ⚠️ Slower            | ✅ Fastest            | ✅ Faster than JIT      |
| Output type                  | IL + runtime binding | Native binary        | IL + native code       |
| Cross-platform deployment    | ✅ Yes                | ⚠️ Platform-specific | ✅ Yes                  |

---

## 🧪 How to Build & Run

### 📥 1. Clone and Restore

```bash
git clone <this-repo>
cd MySolution
dotnet restore
```

---

### ▶️ 2. Run Regular Console App

```bash
dotnet run --project ConsoleApp
```

---

### 🔧 3. Build Native AOT App

```bash
dotnet publish NativeApp -c Release
# Output: NativeApp/bin/Release/net8.0/<RID>/publish/NativeApp.exe
```

> Native AOT apps produce a **fully self-contained native binary**.

---

### ⚙️ 4. Build ReadyToRun App

```bash
dotnet publish R2RApp -c Release
# Output: R2RApp/bin/Release/net8.0/<RID>/publish/R2RApp.exe
```

> Requires runtime installed, but benefits from **precompiled native code** for faster startup.

---

## 🧠 Reflection Support Comparison

| Reflection Feature                 | ConsoleApp | NativeApp  | R2RApp |
| ---------------------------------- | ---------- | ---------- | ------ |
| `typeof(MyType).GetProperties()`   | ✅ Yes      | ✅ Yes      | ✅ Yes  |
| `Assembly.Load("MyAssembly")`      | ✅ Yes      | ❌ No       | ✅ Yes  |
| `Type.GetType("MyNamespace.Type")` | ✅ Yes      | ⚠️ Limited | ✅ Yes  |
| Reflection-based serialization     | ✅ Yes      | ⚠️ Limited | ✅ Yes  |

---

## 🔒 Native AOT Limitations

Native AOT apps **cannot use**:

* `Assembly.Load`, `Assembly.GetExecutingAssembly()`
* Dynamic code generation (`System.Reflection.Emit`)
* Plugins loaded at runtime

To support reflection in Native AOT, you must:

* Use `[DynamicDependency]` attributes
* Avoid runtime assembly loading
* Predefine types and methods to reflect on

---

## 📚 Learn More

* [Native AOT Documentation](https://learn.microsoft.com/dotnet/core/deploying/native-aot/)
* [ReadyToRun Docs](https://learn.microsoft.com/dotnet/core/deploying/ready-to-run)
* [Assembly Trimming & Reflection](https://learn.microsoft.com/dotnet/core/deploying/trimming/prepare-libraries)

---

## 🛠️ Requirements

* .NET 9 SDK+
* Windows/Linux/macOS depending on `RuntimeIdentifier`

---

## 👋 Contact

Feel free to modify or extend this project for your own use cases — such as:

* Plugin systems
* Performance benchmarking
* AOT-safe serialization

Happy coding! ⚙️
