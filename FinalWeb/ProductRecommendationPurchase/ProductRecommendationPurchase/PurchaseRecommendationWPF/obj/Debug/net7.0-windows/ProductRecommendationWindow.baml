   M S B A M L   `   `   `  ����T  PPurchaseRecommendationWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:PurchaseRecommendationWPFPurchaseRecommendationWPF  5   	   ;    5PurchaseRecommendationWPF.ProductRecommendationWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:PurchaseRecommendationWPF     5   	     � Title$   ProductRecomendationWindow��5      $	��450��6+   $	��800��68   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $	��67* �6   �� 5      $��93 �6   .+�5   
   �� -   5         ��Name$ LvProductRecommendation��5       J� OpacityMask
 �����$��2��5   &   $��10, 36, 38, 10q�6:    �� View 5      �� 5      .P�5      �� $��Customer ID��6%    �� Width$	 100��6:    �� DisplayMemberBinding 6F   �� *
CustomerID+�� 5      $��Contact Name��6%   $	 150��6;    6G   �� *ContactName+�� 5      $��Product Name��6%   $	 150��6;    6G   �� *ProductName+�� 5      $��
Product ID��6%   $	 100��69    6E   �� *	ProductID+�� 5      $��Score��6%   $ 50��64    5      �� *Score+�� 5      $��Decision��6%   $ 60��67    6B   �� *
Decision+5      5      �� 5   
   $��Selected Model:��6   $
��Left=�6*   $��23,5,0,0q�6E   $	��Top=�6W   �� 5   
   -   5         ��Name$ txtSelectedModel��5      $��2��6,   $
��Left=�6@   $��21��6[   $��	120,7,0,0q�6g    �� TextWrapping$
 Wrap=�5      $	��Top=�6   $	��332��6-   �� 5   
   -   5        	 ��Name$	 btnPickModel��5      $��
Pick Model��6'   $
��Left=�6<   $��
504,10,0,0q�6W   $	��Top=�6k   6�   5       5!      