# JITBugRepro
Segfaults on Ubuntu ARM64, works fine on my Windows X64 machine

__**Runtime**__
- .NET 7 RC 1

__**Suspects**__
- Executing project using `NonBlocking.ConcurrentDictionary` bundled in referenced project

- `Services.GetRequiredService<MemberDB>()` calls into ctor, which calls ConnectionPool.CreateOrGetConnection("") which reads from a static readonly `NonBlocking.ConcurrentDictionary` field

- `Services.GetRequiredService<InvitesModule>().OnReady()` contains a foreach loop that writes into a `NonBlocking.ConcurrentDictionary`. The write happens after an `await`, which might also be responsible for the weird behavior

- This bug seems to be caused by tiered compilation ( GDB mentioned TC and threadpool thread )
