Unsafe Code in C# - May 2013 TDNH Session
=====================

___Session Summary___

__Stage 1__

* Scanned MSDN docs about Unsafe Code in C#
* Decided to test performance gains using Microsoft's FastCopy code sample: http://msdn.microsoft.com/en-CA/library/aa288474(v=vs.71).aspx
* Unsafe code unexpectedly ran slower than managed code operation.
* Found this SO article explaining performance gains might be marginal http://stackoverflow.com/questions/2760564/why-is-my-unsafe-code-block-slower-than-my-safe-code and reasons unsafe code might be slower. 

__Stage 2__

* We decided to focus on alleged performance gains achievable via unsafe code/pointers by manipulating strings directly.
* Decided to implement a string reversal method. 
* Achieved **20 fold speed increase** in string reversal operation when using unsafe code and pointers compared to a regular managed code implementation. 
* C# project demo'ing fast string reversal with Unsafe code is attached to this github repo. 

__Stage 3__

* Found out .NET string pooling is corrupted when strings (normally immutable) are directly manipulated via pointers. 
* Code sample also includes proof of .NET string pooling corruption. 
* Accompanying SO article - http://stackoverflow.com/questions/16704006/can-string-pooling-be-corrupted-or-confused-by-use-of-unsafe-c-sharp-code

Summary of discoveries about Unsafe Code in C#:
* great performance gains are not guaranteed, is situational dependent 
* marshalling across managed and unsafe code boundary is not necessarily intuitive 
* CLR still protects runtime despite power of pointers 
* C# pointer syntax looks similar to C/C++
* unsafe mode taints the current assembly (C# compiler requires /unsafe flag)
* unsafe mode does not taint other assemblies when referenced 
* an alternative to unsafe code is writing it in C/C++ and calling it

__Errata__

See also: 
* Unsafe code is not unmanaged code -  http://stackoverflow.com/questions/3771042/what-is-difference-between-unsafe-code-and-unmanaged-code-in-c
