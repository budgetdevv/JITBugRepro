# JITBugRepro
Segfaults on Ubuntu ARM64, works fine on my Windows X64 machine

I suspect its due to the executing lib using NonBlocking.ConcurrentDictionary bundled in reference project
