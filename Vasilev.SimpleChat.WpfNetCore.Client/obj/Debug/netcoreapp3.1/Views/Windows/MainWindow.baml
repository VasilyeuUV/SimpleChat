   M S B A M L   `   `   `  ����_  [Vasilev.SimpleChat.WpfNetCore.Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null[2clr-namespace:Vasilev.SimpleChat.WpfNetCore.Client$Vasilev.SimpleChat.WpfNetCore.Client  5   	   q=clr-namespace:Vasilev.SimpleChat.WpfNetCore.Client.ViewModels/Vasilev.SimpleChat.WpfNetCore.Client.ViewModels  5
   	   �Jclr-namespace:Vasilev.SimpleChat.WpfNetCore.Client.Infrastructure.Commands<Vasilev.SimpleChat.WpfNetCore.Client.Infrastructure.Commands  5   	   5    /Vasilev.SimpleChat.WpfNetCore.Client.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @l2clr-namespace:Vasilev.SimpleChat.WpfNetCore.Client     5   	   Lvm=clr-namespace:Vasilev.SimpleChat.WpfNetCore.Client.ViewModels     5
   	   ZcmdJclr-namespace:Vasilev.SimpleChat.WpfNetCore.Client.Infrastructure.Commands     5   	   $	��450��5   	   $	��350��6   $	��500��5	   	   $	��400��6     � Title  5   	   �� *Title+ +� FocusedElement 5   	   ��  �� ElementName$ tbxMain�� � DataContext 5      I  CVasilev.SimpleChat.WpfNetCore.Client.ViewModels.MainWindowViewModel  5   
    J� InputBindings 5      �� 5   
    �� 	Modifiers$
 Ctrlf�6   
 �� Key$ Q��6&    �� Command 5      �� *+)MainMenuViewModel.CloseApplicationCommand+.��5      ]� .+�5   
   �� $��Bottom=�6   .@�5      �� .��5      �� ��6   �� *Client.Connection.IsConnected+ �� StringFormat$ Connected: {0}��	 �� UpdateSourceTrigger$	 PropertyChanged=�5      �� 5!      $��20,0,0,0q�6   .��5"      �� ��6   �� *Client.Connection.Ip+$ IP: {0}��5#      �� 5&      $��20,0,0,0q�6   .��5'      �� ��6   �� *Client.Connection.Port+$ 	Port: {0}��5(      5*      � 5,   
   
 � ColumnDefinitions
 5.      �� 5/       � RowDefinitions 51      �� 52      $
��Auto �6    �� 53      $��* �6    �� 54      $
��Auto �6    .+�58      � $��0��6   $��0��6    $��3q�59      
 5;      �� 5<      $��* �6'   �� 5=      $
��Auto �6'   �� 5>      $
��Auto �6'    5@      �� 5A      $��* �6$   .+�5D      �� $��0��6   $��5q�5F      ��5E      �� *Client.UserName+$ 	User: {0}��5F      �� 5H      $��2��6   $
��Exit��5I      $��3q�5K      ��5J      �� *CloseApplicationCommand+5K      5M      �� 5P      -   5         ��Name$ lbMain��5Q      $��1��5P      $��3q�5R      ��5S      �� *Client.Chat+ �� SelectedItem 5T      �� *Client.SelectedMessage+��5W      �� 5X      .R�5Y      �� 
����ح�
��� � �$��2q�5[   !    �� CornerRadius$ 5��5\   !   ._�5^      �� $��5q�6)    �� Orientation$ Vertical=�5_   )   .+�5a   "   �� 
��� � �$��3,3,3,0q�5c   ,   $��14(�5d   ,   ��5a   ,   �� *Author+5d   ,   �� 5f   "   
��� � �$��3,3,3,0q�5h   ,   $��10(�5i   ,   ��5f   ,   �� *Dtg+$ dd.MM.yyyy HH:mm��5i   ,   �� 5k   "   �� 5l   "   $
��Wrap=�5m   ,   $��3q�5n   ,   $	��200��5o   ,   $��14(�5p   ,   ��5l   ,   �� *	Message+5p   ,   5r      5s      5t      5w      � 5z      $��2��6   $��3q�5{      
 5}      �� 5~      $��* �6'   �� 5      $
��Auto �6'    5�      �� 5�      $��* �6$   .+�5�      �� -   5         ��Name$ tbxMain��5�      $��0��5�       �� 	MaxLength$
 1000��5�       �� TextWrapping$
 Wrap=�5�       �� AcceptsReturn$
 True��5�      $
��Auto=�5�      $��65��5�      $��3q�5�      ��5�      �� *Client.UserMessage+ �� Mode$ TwoWay=�$	 PropertyChanged=�.��5�       5�      �� 5�      $ Return��6%   $
 Ctrlf�62    5�   %   �� *SendMessageCommand+ �� CommandParameter 5�   %   �� *Client.UserMessage+.��5�   -   5�      �� 5�      $��1��6   $
��Send��5�      $��3q�5�      ��5�      �� *SendMessageCommand+��5�      �� *Client.UserMessage+5�      5�      5�      5�      5�      