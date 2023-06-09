# CefSharp-TinyMCE
展示Winform使用CefSharp组件集成TinyMCE编辑器，并演示C#与JavaScript传值交互的多种方法。
使用的框架是.NET Framework 4.7.2.

### 1. 功能&截图
界面如下图，主要功能说明如下，其它内容可以参考界面元素旁边的描述。
- 点击“测试弹出框”可以读取“输入框1”的值并由C#弹窗。
- 点击“读取信息”可以读取C#定义参数并由JS弹窗。
- 调整Winform应用程序窗口的大小，可以看到2个输入框的值都在随着变动。
- 点击底部“提交”按钮，会把TinyMCE的内容提交到后台并进行C#弹窗。
![image](https://github.com/CookByte/CefSharp-TinyMCE/assets/16180316/8326dfa4-a1a3-4814-a1bf-b6aefc19e707)

### 2. 提示
- 给Winform设计界面的窗口属性绑定SizeChanged事件。  
![image](https://github.com/CookByte/CefSharp-TinyMCE/assets/16180316/b7bec778-36e2-4c51-bc55-de6850e63626)
- 详细使用文档可以参考：https://zhuanlan.zhihu.com/p/628209406
