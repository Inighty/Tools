for /f "tokens=*" %%a in ('dir obj /b /ad /s ^|sort') do rd "%%a" /s/q
for /f "tokens=*" %%a in ('dir bin /b /ad /s ^|sort') do rd "%%a" /s/q
for /f "tokens=*" %%a in ('dir binr /b /ad /s ^|sort') do rd "%%a" /s/q
