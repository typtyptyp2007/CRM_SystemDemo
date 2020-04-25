# CRM_SystemDemo
本程序是简易版CRM系统，纯粹娱乐开发

适合c#初学者学习

1. 使用指南：
   - 执行 script.sql
   - 配置相应数据库连接
   - 省、市相关字段未完善
   - 部分下拉框内容未完善

2. 数据库使用SqlServer

3. 需要添加管理员账号才能登陆，具体sql如下：

   ```sql
   insert manager values ('abc',substring(sys.fn_sqlvarbasetostr(HashBytes('MD5','123')),3,32));
   ```

   abc ： 管理员账号

   123 ： 管理员密码

   

4. 有什么问题可以直接提issue

    

    

   