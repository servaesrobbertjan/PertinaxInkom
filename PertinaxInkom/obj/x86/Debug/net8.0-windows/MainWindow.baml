   M S B A M L   `   `   `  ����H  DPertinaxInkom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:PertinaxInkomPertinaxInkom  5   	       PertinaxInkom.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:PertinaxInkom     5   	     � Title$  Pertinax Inkom��5   	   $
��1080��6    $
��1920��6.    � WindowState$ 	Maximized=�6;    � WindowStyle$ SingleBorderWindow=�6S    � Icon$0 *pack://application:,,,/Images/pertinax.ico��5	   	    � 	Resources 5         ButtonImage �� ImageSource �� 
TargetType  PaperButton �� Property �� Value	 �� 
TargetType
 J� SnapsToDevicePixels �� Triggers P� Property P� Value��%1  &	        &	 O     7   �� 5   
   $6 0pack://application:,,,/Images/whitepaperback.png��6)   6j   �� 5   
   " ��6   .�5      ��  � ��8   6R   �� 5       � �� 5      �� 5      "	 ��6&   .R�5      �� #��z��6!   #��z��6K   #��z��6w   ._�5      �� #��z��6/   #��z��5      #��z��68   #
 z
 5      $
��True��6Q   5       5      P� 5       �@J�IsMouseOver$
 True��6=   . �5   "   ��  � �� �#1313136?   �� 5   "    �@�Cursor$
 Hand��6;   5      5      5       5!      5"      .��5$      ]� .+�5%   
   � -   5        $��Mainleft��5%      $
��Left=�6   ��5&      �� 5'      $  /Images/blackpaperback.jpg��6    �� Opacity$ 1Q�6F    � RowDefinitions 5)      �� 5*      $��* �6    �� 5+      $
��auto �6    .+�5-      �� .+�5.      �� -   5        $��btnVisitors��5.      #��[ 6F   $��5,5,5,5q�6k   $
��Left=�6|   ��5/      � 50       � ColumnDefinitions 51      �� 52   "   $��* �63   �� 53   "   $��* �63   .+�55      �� $0��*pack://application:,,,/Images/Bezoeker.png��6$   $��0��6X   $��50��6h   $	��100��6w   �� 56      $��Visitors��6(   $��1��68   $��Center=�6H   $��20(�6c    	 Anton#��[ 6q   57      59      �� 5:      -   5        $��btnParticipant��5:      #��[ 6L   $��5,5,5,5q�6q   $
��Left=�6�   ��5;      � 5<       5=      �� 5>   "   $��* �63   �� 5?   "   $��* �63   .+�5A      �� $1��+pack://application:,,,/Images/Deelnemer.png��6$   $��0��6Y   $	��100��6i   �� 5B      $��Participants��6(   $��1��6<   $��Center=�6L   $��20(�6g   #��[ 6u   5C      5E      �� 5F      -   5        $��
btnReprint��5F      #��[ 6D   $��5,5,5,5q�6i   $
��Left=�6z   ��5G      � 5H       5I      �� 5J   "   $��* �63   �� 5K   "   $��* �63   .+�5M      �� $0��*pack://application:,,,/Images/Herprint.png��6$   $��0��6X   $	��100��6h   �� 5N      $��Reprint Wristband��6(   $��1��6A   $��Center=�6Q   $��20(�6l   #��[ 6z   5O      5Q      5R      �� 5S      $��1��6   $��Bottom=�6&   .+�5T      �� -   5        $��btnSettings��5T      #��[ 6F   $��5,5,5,5q�6k   $
��Left=�6|   ��5U      � 5V       5W      �� 5X   "   $��* �63   �� 5Y   "   $��* �63   .+�5[      �� $0��*pack://application:,,,/Images/Settings.png��6$   $��0��6X   $	��100��6h   �� 5\      $��Settings��6(   $��1��68   $��Center=�6H   $��20(�6c   #��[ 6q   5]      5_      �� 5`      $��V.Z.W. Pertinax Esports��6   $��10,0,0,0q�6;   $��20(�6M   
�������6p   �� 5a      $��De Gavers 28��6   $��10,0,0,0q�60   $��20(�6B   
�������6e   �� 5b      $��9750 Kruisem��6   $��10,0,0,0q�60   $��20(�6B   
�������6e   �� 5c      $��BE 1009.136.926��6   $��10,0,0,0q�63   $��20(�6E   
�������6h   5d      5e      � 5f   
   -   5        $��	MainRight��5f      $��Right=�6    ��5g      �� 5h      $6 0pack://application:,,,/Images/whitepaperback.png��6   $ 1Q�6\   .+�5j      �� $0��*pack://application:,,,/Images/pertinax.ico��6    J� Opacity$	 0.5Q�6H   $��20q�6V   5k      5l      5m      