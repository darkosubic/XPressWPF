   M S B A M L   `   `   `  ���� K  GXPressWPF.Shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM8clr-namespace:XPressWPF.Shared;assembly=XPressWPF.SharedXPressWPF.Shared   XPressWPF.Modules2   ,XPressWPF.Modules.Employee.View.EmployeeView   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    >d2http://schemas.microsoft.com/expression/blend/2008    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     N	resources8clr-namespace:XPressWPF.Shared;assembly=XPressWPF.Shared     X TXceed.Wpf.Toolkit, Version=3.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit      DefaultUserControlBackground#���   .��%  Xceed.Wpf.Toolkit.BusyIndicator      IsBusy  �� *	IsWorking+.���  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��* � � ColumnDefinitions �� $
��Auto ��� $��* �.+��� $��1��$��0��   XPressWPF.Shared.Labels   LBL_FirstName#��Z  �� ContentStringFormat$
 {0}:���� $��1��$��1������ *CurrentEmployee.FirstName+�� $��2��$��0��   LBL_LastName#��Z $
 {0}:���� $��2��$��1������ *CurrentEmployee.LastName+�� $��3��$��0��   LBL_Age#��Z $
 {0}:���� $��3��$��1������ *CurrentEmployee.Age+�� $��4��$��0��   
LBL_Salary#��Z $
 {0}:���� $��4��$��1������ *CurrentEmployee.Salary+�� $��5��$��0��   LBL_DepartmentName#��Z $
 {0}:���� $��5��$��1������ * CurrentEmployee.DepartmentName+�� $��6��$��0��$��2��	 �� Orientation$	 
Horizontal=�.+��� ���� *RefreshEmployeesCommand+.��	Refresh�� ���� *AddNewEmployeeCommand+.��Add�� ���� *UpdateEmployeeCommand+.��Update�� ���� *DeleteEmployeeCommand+.��Delete�� $��7��$��0��$��2��  GridViewItemContStyle#��[ ���� *	Employees+
 �� SelectedItem
 �� *CurrentEmployee+ �� View ��  �� AllowsColumnReorder .   �� ColumnHeaderToolTip$ 	Employees��.P���    LBL_ID#��Z  �� Width$
 Auto�� �� DisplayMemberBinding �� *Id+�� #��Z $
 Auto�� �� *	FirstName+�� #��Z $
 Auto�� �� *
LastName+�� #��Z $
 Auto�� �� *Age+�� #��Z $
 Auto�� �� *Salary+�� #��Z $
 Auto�� �� *DepartmentName+