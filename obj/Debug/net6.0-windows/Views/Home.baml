   M S B A M L   `   `   `  ����@  <SIMMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:SIMMS.ViewsSIMMS.Views  5      O,clr-namespace:SIMMS.Modules.HomeModule.ViewsSIMMS.Modules.HomeModule.Views  5          SIMMS.Views.Home   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:SIMMS.Views     5      Z VPrism.Unity.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59T PPrism.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/   5      X	 TFontAwesome.Sharp, Version=6.3.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10fFfa=http://schemas.awesome.incremented/wpf/xaml/fontawesome.sharp 	 5
      ?activityCard,clr-namespace:SIMMS.Modules.HomeModule.Views 	 5      !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  True5	      .��5      �� ._�5   
   �  � RowDefinitions 5      �� 5      $��50 �6    �� 5      .+�5      �� $��0��6   ._�5      �� $��Center=�6   $��Center=�5         ForegroundText3#��[  5      $��30��5      $	��500��5       �� CornerRadius$ 	0,0,30,30��5      ._�5      �� $��ACTIVIDADES DEL USUARIO��6    $��Center=�5      $��Center=�5      $��24(�5       
�������$��Cascadia Code)�5!   /   $
��Bold"�6J   5"      5#      � 5$      $��1��6    � ColumnDefinitions 5%      �� 5'      $	��350 �6'   �� 5)      .+�5+      �� $ 10��6   $��0��5,      ��5.      �� 5/       �� 
StartPoint$ 0,0.5&�6.    �� EndPoint$	 1,1&�6A   .��50      �  � Color$ #b3ccc1��6+   � 51      $ #669999��6+   52      ._�55      �  56      �� 57      �� 58      .+�5:      A� $	��200��6"   $	��200��5;   "   $��0��5<   "   $��1��5=   "   
����� ���5@      �� 5A   "    �� ImageSource 6-   �� *CurrentUserAccount.Profile+5E      � 5F      $��1��6    5G      �� 5H   "   $��10 �60   �� 5I   "   �� 5J   "   .+�5L      � $��0��6#   .+�5M   "   �� 	 �� X1$ 10��6'   		 �� X2$		 270��6/   	
 �� Y1$
 10��5N   '   	 �� Y2$ 10��6/   $��Center=�5O   '   $��Center=�5P   '   $��2��5Q   '   ���#82A3A35R   '   5S      � 5T      $��1��6#    5U   "   �� 5V   &   �� 5W   &   �� 5X   &   �� 5Y   &   .+�5[   "   �� $��0��6-    �� Orientation$ 
Horizontal=�5\   -   $��20��5]   -   .+�5^   &   �� $��Nombre:��60   
������� �� Padding$ 10,0,0,0q�5`   0   $
��Bold"�6C   �� 5a   &   $��	Monserrat)�5b   0     TextBlockStyle#��[ 5c   0     ButtonFlagOrange2
 � Tag# [ 5d   0   $ 10,0,0,0q�5e   0   $��16(�5f   0   ��5a   0   �� *DisplayName+5f   0   5h   #   �� 5i   "   $��1��6-   $ 
Horizontal=�5j   -   $��20��5k   -   .+�5l   &   �� $��Usuario:��60   
�������$��10,0,0,0q�5n   0   $
��Bold"�6B   �� 5o   &   $��10,0,0,0q�5p   0   #��[ 5q   0   # [ 5r   0   $��Consolas)�5s   0   $��16(�5t   0   ��5o   0   �� *CurrentUserAccount.Username+5t   0   5u   #   �� 5v   "   $��2��6-   $ 
Horizontal=�5w   -   $��20��5x   -   .+�5y   &   �� $��Email:��60   
�������$��10,0,0,0q�5{   0   $
��Bold"�6B   �� 5|   &   $��10,0,0,0q�5}   0   #��[ 5~   0   # [ 5   0   $��16(�5�   0   ��5|   0   �� *CurrentUserAccount.Email+5�   0   5�   #   �� 5�   "   $��3��6-   $ 
Horizontal=�5�   -   $��20��5�   -   .+�5�   &   �� $��Ocupacion en la empresa:��60   
�������$��10,0,0,0q�5�   0   $
��Bold"�6B   �� 5�   &   $��10,0,0,0q�5�   0   $��	Monserrat)�5�   0   #��[ 5�   0   # [ 5�   0   $��16(�5�   0   ��5�   0   �� *CurrentUserAccount.Position+5�   0   5�   #   5�      � 5�      $��2��6#   .+�5�   "   �� $ 
Horizontal=�6-   $��Center=�5�   -   $��Center=�5�   -   $��10,0,0,0q�5�   -   .+�5�   &   ��   
ButtonFlag#��[ 6-    � Cursor$
 Hand��5�   )   $��DataBase��5�   -   $��Center=�5�   )   $��Center=�5�   )   ��5�   )   �� *
Navigate+.��5�   *   �� $ Vertical=�65   $��Center=�5�   1   $��Center=�5�   1   .+�5�   .   ! 	 FontAwesome.Sharp.IconImage  � 	 �FontAwesome.Sharp.IconImageBase`1[[FontAwesome.Sharp.IconChar, FontAwesome.Sharp, Version=6.3.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10f]]   Icon$ NotesMedical=�6;     menuButtonIcon2#��[ 6O   5�   +   5�   '   �� 5�   &   #��[ 6-   $
 Hand��5�   )   $��Center=�5�   )   $��Center=�5�   )   $��20,0,0,0q�5�   -   ��5�   -   �� *EditUserInfoCommand+.��5�   *   �� $ Vertical=�65   $��Center=�5�   1   $��Center=�5�   1   .+�5�   .     $
 Edit=�6;   #��[ 6G   5�   +   5�   '   �� 5�   &   #��[ 6-   $
 Hand��5�   )   $��Center=�5�   )   $��Center=�5�   )   $��20,0,0,0q�5�   -   .��5�   *   �� $ Vertical=�65   $��Center=�5�   1   $��Center=�5�   1   .+�5�   .     $ Trash=�6;   #��[ 6H   5�   +   5�   '   5�   #   5�      5�      5�      5�      � 5�      $��1��6    5�      �� 5�      $��40 �6(   �� 5�      .+�5�      � $��10,0,0,0q�6    5�      �� 5�      $	��.2* �6/   �� 5�      $	��.7* �6/   �� 5�      $	��.2* �6/   .+�5�      �� $��Buscar: ��6$   $��Right=�5�   $   $��Center=�5�   $   $��18(�5�   $   $
��Bold"�5�   $   #��[  5�   $   $
��Wrap=�5�   $   �� 5�      -   5         ��Name$ tbSearch��5�   "   $��0,0,10,0q�5�   "   $��14(�5�   "   $��1��5�   "   
�������
������  J� Opacity$	 0.8Q�5�   "    �� TextAlignment$ Center=�5�   "    �� TextWrapping$
 Wrap=�5�   "     TextBoxRoundedStyle2#��[ 5�   "   ��5�   "   �� *	TxtSearch+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�.��5�   9   5�      � 5�      $��2��6    5�      �� 5�   "   $	��.4* �60   �� 5�   "   $	��.6* �60   .+�5�      �� $��Ordenar por:��6(   $��Center=�5�   (   #��[  5�   (   $��0��5�   (   �� 5�      $��1��5�   #   ��5�   #   �� *SortValuesBy+ �� SelectedItem 5�   #   �� *	SortValue+5�      5�      5�      � 5�      $��1��6   $��10q�5�       5�      �� 5�      $	��.7* �6,   �� 5�      $	��.3* �6,   .+�5      �� $
��Auto=�6'   .��5     �� ��6+   �� *" ActivityCardObservableCollection+��5  "   �� 5  &   .R�5  *   1   +SIMMS.Modules.HomeModule.Views.ActivityCard  $��0,5,0,5q�6D   5  '   5     5     � 5     $��1��6   .+�5     �� ���#5fbab9$ 	20,20,0,0��5  %   ._�5  "   �  5  &   �� 5  *   �� 5  *   �� 5  *   $	��.4* �6;   .+�5  &   � $��0��6+   $��	0,20,0,10q�5  +    5  *   �� 5  .   $
��auto �6<   �� 5  .   �� 5  .   �� 5  .   .+�5  *   �� $��5,0,5,0q�64   $
��Bold"�5  4   $��14(�5  4   $��Nombre de la actividad��5  5   
�������$��Center=�5!  4   �� 5"  *   -   5        $ 
tbActivity��5#  1   $��5,0,5,0q�5"  2   $��14(�5%  1   $��1��5&  1   ���#e7e7e7
������ $	 0.8Q�5)  1   $ Center=�5*  1   $
 Wrap=�5+  1     TextBoxRoundedStyle#��[ 5,  1   ��5$  2   �� *ActivityTitle+.��5-  G   5/  +   �� 50  *   $ 
Horizontal=�65   $��2��51  5   $��Center=�52  5   .+�53  .   �� $��
Fecha: De ��68   $��14(�54  8   $��5,0,0,0q�55  8   
�������$
��Bold"�57  8   (  "System.Windows.Controls.DatePicker  58  .   $��90��69   $��5,0,0,0q�59  9      SelectedDate 5:  9   �� *
DateFrom+�� 5<  .   $��A��68   $
��Bold"�5=  8   
�������$��14(�5?  8   $��5,0,0,0q�5@  8     5A  .   $��90��69   $��5,0,0,0q�5B  9    5C  9   �� *DateTo+5D  +   �� 5E  *   $ 
Horizontal=�65   $��3��5F  5   $��Center=�5G  5   .+�5H  .   �� $��
Prioridad:��68   $
��Bold"�5I  8   
�������$��14(�5K  8   $��5,0,0,0q�5L  8   �� 5M  .   $	��100��67   $��5,0,0,0q�5N  7   ��5O  7   �� *ValuesPriority+ 5P  7   �� *PriorityValue+5R  +   5S  '   � 5T  &   $��1��6+   $��	0,20,0,10q�5U  +    5V  *   �� 5W  .   $
��auto �6<   �� 5X  .   �� 5Y  .   �� 5Z  .   .+�5\  *   �� $��5,0,5,0q�64   $��14(�5]  4   $
��Bold"�5^  4   
�������$!��Descripcion de la actividad��5`  -   $��Center=�5a  4   �� 5b  *   -   5        $ tbActivityDescription��5c  1   $��5,0,5,0q�5b  2   $��14(�5e  1   $��1��5f  1   ���#e7e7e7
������ $	 0.8Q�5i  1   $ Center=�5j  1   $
 Wrap=�5k  1   #��[ 5l  1   ��5d  2   �� *ActivityDescription+.��5m  G   5o  +   �� 5p  *   $ 
Horizontal=�65   $��2��5q  5   $��Center=�5r  5   $
��Left=�5s  5   $��5,0,0,0q�5t  5   .+�5u  .   d� $��80��6;   $��32��5w  7   ���#e7e7e7  ToggleButtonStyle#��[ 5y  7    d� 	IsChecked 5v  7   �� *IsCompleted+5y  7   �� 5{  .   $��Completado?��68   $��5,0,0,0q�5|  8   
�������$��14(�5~  8   $
��Bold"�5  8   $��Center=�5�  8   5�  +   5�  '   � 5�  &   $��2��6+   .+�5�  *   �� $��80��5�  1   $��80��5�  1     ButtonRounded#��[ 5�  1   $��Center=�5�  1   ��5�  1   �� *AgreeActivityCommand+.��5�  .   �� $ Vertical=�69   $��Center=�5�  9   $��Center=�5�  9   .+�5�  2     $
 Plus=�6?   $��Center=�5�  @   $��Center=�5�  @   #��[ 5�  @   �� 5�  2   $��Añadir��6<    �� TextAlignment$ Center=�5�  <   6U   5�  /   5�  +   5�  '   5�  #   5�     5�     5�     5�     5�     5�     5�     5�     