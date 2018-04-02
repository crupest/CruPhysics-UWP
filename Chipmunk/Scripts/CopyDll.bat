@echo off
cd ..\..
echo Copy Chipmunk.dll to CruPhysics:
copy .\Release\Chipmunk\Chipmunk.dll .\CruPhysics\
echo Copy Chipmunk.dll to CruPhysicsUnitTest:
copy .\Release\Chipmunk\Chipmunk.dll .\CruPhysicsUnitTest\
pause
