
DSA Resource 檔案。
====================

支援下列兩種格式：

1. 通用資源「*.rc.xml」，例：

rc1.rc.xml

<Resource Name="rc1"><![CDATA[
	select * from student limit 10;
]]></Resource>


2. 專用格式，支援下列「*.txt、*.sql、*.js、*.json」，例：

getStudent.sql
moduleA.js


主檔名即是 Resource 名稱，此例中是「getStudent、moduleA」。