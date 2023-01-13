# csharp-coding-assignment
Implement WhatIsTheTime method which will return analog clock in the mirror position.

Examples
| Input | Expected result |
| ----------- | ----------- |
| 05:25 | 06:35 |
| 01:50 | 10:10 |
| 11:58 | 12:02 |
| 12:01 | 11:59 |

Notes
- Hours should be between 1 <= hour <= 12
```0:20``` should be ```12:20```
```13:20``` should be ```01:20```
             
## 1a
Invalid input value the method will return error message instead.

| Input | Output |
| ----------- | ----------- |
|```05:89``` | ```Invalid input!``` |
     
## 1b
Let method accept $ symbol as 3.
| Input | Output |
| ----------- | ----------- |     
| ```03:25``` | ```08:35``` |
| ```0$:25``` | ```08:35``` |
     
## 2 
Add support for words ("one", "two", "three",...)

| Input | Output |
| ----------- | ----------- |     
| ```1:20``` | ```11:40``` |
| ```one twenty``` | ```11:40``` |
     
## 3 
Add support for special english time names.
    
```half past eight``` equals  ```08:30```
```quarter to eight``` equals ```07:45```
     
## 4 
Add support for mutiple inputs in string separated by ;; symbol. The return value will be always in the numeric form.
| Input | Output |
| ----------- | ----------- |   
|  ```05:25;;one fifty;;11:58``` | ```06:35;;10:10;;12:02``` |
       
## 4a
If there are two same times in the colletion the method will not include mirror values in the result .
| Input | Output |
| ----------- | ----------- |   
| ```05:25;;five twentyfive``` | ```06:35``` |
     
## Final words: 
- Dont be afraid to try some design patterns.
- Sometimes less is more :)
- Dont forget to validate inputs!
- Feel free to add any additional test cases you want! The more the better :) 
         