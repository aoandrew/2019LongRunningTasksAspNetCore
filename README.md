# 2019LongRunningTasksAspNetCore
2019 Long Running Tasks Asp.Net Core

Two appropriate methods to deal with long running tasks and Asp.Net core:
Method 1:
https://www.jerriepelser.com/blog/communicate-status-background-job-signalr/ (this is a good solution if you were okay tying the long-running task to the web app directly).  This exploration uses a project called Coravel - which I'll explore - but it references Hangfire which on github has a much, much bigger following. In the future, I will use that. 

Method 2:
https://www.jerriepelser.com/blog/communicate-from-azure-webjob-with-signalr  (this is a good overview of how to do a long-running task in the background-- however, now, instead of using a Queue and WebJob or a Queue and a regular Azure Function, you would want to use a Azure Durable Function)

