   M S B A M L   `   `   `  ����G  CConnect4Game, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:Connect4Game.ViewsConnect4Game.Views  5      (    "Connect4Game.Views.CustomAlertView   5      \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      M IFontAwesome.Sharp, Version=5.15.3.0, Culture=neutral, PublicKeyToken=nullFfa=http://schemas.awesome.incremented/wpf/xaml/fontawesome.sharp  5      ,local clr-namespace:Connect4Game.Views  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5        � Title$  Alert��5	      $	��400��5
      $	��150��5      
������� J� Opacity$	 0.9Q�5       J� OpacityMask
 ����� � 
ResizeMode$ NoResize=�5       � WindowStartupLocation$ CenterScreen=�5       � WindowStyle$
 None=�5      '  !System.Windows.Shell.WindowChrome   WindowChrome 5        5   
      CornerRadius$ 20��6   .��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   	 � RowDefinitions	 5    
   �� 5"      $
��Auto �6   �� 5#      $��* �6   .+�5'   
   �� $��0��5(      $��0��5)      $��2��5*      $��
10,0,10,10q�5+      $��Center=�5,      $��Center=�5-      $��Alert��5.      $��25)�5/         FontSizeLarge#��[  50      $
��Bold"�51      
�������52      �� 54   
   -   5        
 ��Name$
 CloseCommand��55      $��0��56      $��1��57      $��5q�58      $��Center=�59      $��Center=�5:        menuCloseCtrl#��[ 5;      ��5=      �� 5>      ._�5?      !  FontAwesome.Sharp.IconImage  $��20��5@      $��20��5A      �  �FontAwesome.Sharp.IconImageBase`1[[FontAwesome.Sharp.IconChar, FontAwesome.Sharp, Version=5.15.3.0, Culture=neutral, PublicKeyToken=null]]   
Foreground
 �����   Icon$ WindowClose=�5C      $	 0.8Q�5D      5E      5G      �� 5I   
   -   5         ��Name$ Message��5J      $��1��5K      $��0��5L      $��2��5M      $��Center=�5N      $��Center=�5O        LatoRegular#��[ 5P      $��25(�5Q      $
��Bold"�5R        WorldBlueBrush#��[ 5S      5U      5W      