   M S B A M L   `   `   `  ����+  'Recommender System for e-commerce sites8    2Recommender_System_for_e_commerce_sites.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$-  'Recommender System for e-commerce sites��5   	   $��379.682��69   $��588.074��6J   .��5      � .+�5   
   �� .@�5      �� -   5         ��Name$ dataInputTab��5      $��
Data Input��6,   $��-2,-2,-28,0q�6@   .��5      � ���	#FFE5E5E5.+�5	      �� -   5         ��Name$ Read_Transactions��5	      $��Read Transactions��68   $
��Left=�6T   $��
350,81,0,0q�6o   $	��Top=�6�   $	��144��6�   $��26��6�   �� 5
      -   5        $ Process_transactions��5
      $��Process Transactions��6;   $
��Left=�6Z   $��350,127,0,0q�6u   $	��Top=�6�   $	��144��6�   $��26��6�   �� 5      $��Support and Confidence��6   $��10,168,42,0q�6?   $	��Top=�6T   $	��141��6l   $��3��6y   .��5      � $
��Left=�6   $	��131��6:   $	��Top=�6G   $	��518��6_   $��
0,-12,-2,0q�6k   .+�5      �� -   5         ��Name$ 
SuppSlider��5   %   $��198,26,10,0q�69    � Minimum$ 1Q�6N    � Maximum$	 100Q�6Z   $��28��6h   $	��Top=�6�    � Value 6t   �� *
MinSupport+6�   �� 5      -   5        $ ConfiSlider��5   %   $��198,72,10,0q�6:   $ 1Q�6O   $	 100Q�6[   $��28��6i   $	��Top=�6�    6u   �� *MinConfidence+6�   �� 5      $��Min Support��6$   $
��Left=�6:   $��	10,26,0,0q�6U   $	��Top=�6h   $��28��6�   $��80��6�   �� 5      $��Min Confidence��6$   $
��Left=�6=   $��	10,72,0,0q�6X   $	��Top=�6k   $��28��6�   $��94��6�   �� 5      -   5         ��Name$ 	SuppLabel��5   $   $
��Left=�6�   $��
132,26,0,0q�6�   $	��Top=�6�   $��48��6�   ��67   ��  �� ElementName$ 
SuppSlider��	 �� Path$	 Value�
 �� UpdateSourceTrigger$
 PropertyChanged=�6�   �� 5      -   5        $ 
ConfiLabel��5   $   $
��Left=�6�   $��
132,72,0,0q�6�   $	��Top=�6�   $��48��6�   ��68   �� $ ConfiSlider��$	 Value�$
 PropertyChanged=�6�   5      5      �� 5      -   5        $ BrowseButton��5      $��Browse data file(.csv)��63   $
��Left=�6T   $��
439,26,0,0q�6o   $	��Top=�6�   $	��123��6�   $��23��6�   �� 5      -	   5         ��Name$ FileNameTextBox��5      $
��Left=�67   $��23��6R   $��	10,26,0,0q�6^    �� TextWrapping$
 Wrap=�6q   $�� ��6�   $	��Top=�6�   $	��381��6�   �� 5      $?��9Click this button to read transactions from the .csv file��6   $
��Left=�6`   $��	10,81,0,0q�6{   $	��Top=�6�   $	��319��6�   �� 5      $>��8Click this button to process transactions and form rules��6   $
��Left=�6_   $��
10,127,0,0q�6z   $	��Top=�6�   $	��319��6�   5      5      �� 5      -
   5        $ QueryTab��5      $��Query��6(   $��29,-1,-64,-1q�67    J� RenderTransformOrigin$ 0.5,0.5&�6M   z�5      V� 5       .~�5!      �� �� 5"      �� 5#       �� Angle$ -0.715Q�6*   T� 5$      5%      .��5'      � ���	#FFE5E5E5.+�5(      �� -   5        $ queryTextBox��5(      $
��Left=�64   $��23��6O   $��
110,49,0,0q�6[   $
 Wrap=�6o   $�� ��6�   $	��Top=�6�   $	��322��6�   �� 5)      $��Enter the item purchased��6   $
��Left=�6?   $��
110,18,0,0q�6Z   $	��Top=�6n   $	��161��6�   �� 5*      -   5        $ suggestionsButton��5*      $��View Suggestions��68   $
��Left=�6S   $��
452,49,0,0q�6n   $	��Top=�6�   $	��112��6�   $��23��6�   6�   �� 5+      -   5        $ resultLabel��5+      $�� ��61   $
��Left=�6<   $��
89,157,0,0q�6W   $	��Top=�6k   $��85��6�   $	��363��6�   �� 5,      -   5        $ otherSuggestionsButton��5,      $��Other suggestions��6=   $��457,193,0,0q�6Y   $	��Top=�6n   $��29��6�   $
��Left=�6�   $	��107��6�   �� 5-      $��Results:��6   $
��Left=�6/   $��110,112,0,0q�6J   $	��Top=�6_   $	��189��6w   �� 5.      -   5        $ View_Item_List��5.      $��View Item List��65   $
��Left=�6N   $��457,112,0,0q�6i   $	��Top=�6~   $	��107��6�   $��26��6�   6�   5/      50      51      52      53      