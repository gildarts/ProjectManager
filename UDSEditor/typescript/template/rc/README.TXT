
DSA Resource 檔案。
====================

支援下列兩種格式：

1. 通用資源「*.rc.xml」，例：

rc1.rc.xml

<Resource Name="rc1"><![CDATA[
	select * from student limit 10;
]]></Resource>

使用這種通用格式要特別小心，如果格式不正確將不會被儲存，也不會有錯誤訊息。

2. 專用格式「*.txt、*.sql、*.js、*.json ...任何文字格式」
例：

getStudent.sql
moduleA.js

主檔名即是 Resource 名稱，此例中是「getStudent、moduleA」。
主檔名請勿再使用「.」(例: student.get.sql)，否則可能產生覆蓋、錯誤、遺失等問題。