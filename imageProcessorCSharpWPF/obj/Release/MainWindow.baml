   M S B A M L   `   `   `  ���� R  NimageProcessorCSharpWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:ImageProcessorCSImageProcessorCS  !    ImageProcessorCS.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     0localclr-namespace:ImageProcessorCS       � Title$  
MainWindow��$	��900��$
��1350�� � 
ResizeMode$ CanMinimize=�.��� 
������� � ColumnDefinitions �� $
��321* ��� $��1004* � � RowDefinitions �� $	��87* ��� $
��450* ��� $
��302* �.+��� -    ��Name$ Image_Group��$��Image��$
��Left=�$��
16,10,0,46q�$	��971��$��3��$��1��.���� -   $��ImageBorder��}�. ._��� -    ��Name$ imageBox��$��15��$��15��$y�Visible=�$��1��$��10,10,-2,18q�$��1�� J� RenderTransformOrigin$ 0.5,0.5&�z�V� -   TG���� -    ��Name$
 Save��$��
Save Image��$��1��$
��Left=�$��35��$��828,273,0,0q�$��2��$	��Top=�$	��150���� -    ��Name$
 XTxt��$��1��$
��Left=�$��20��$��253,273,0,0q�$��2��$
��Wrap=�$�� ��$	��Top=�$��64���� -   $
 YTxt��$��1��$
��Left=�$��20��$��322,273,0,0q�$��2��$
��Wrap=�$�� ��$	��Top=�$��67���� -   $ ValueTxt��$��1��$
��Left=�$��20��$��394,273,0,0q�$��2��$
��Wrap=�$�� ��$	��Top=�$	��124���� -	   $ Reset��$��	Reset All��$��1��$
��Left=�$��37��$��
10,271,0,0q�$��2��$	��Top=�$	��150���� -
   $ openFileButton��$��	Open File��$��30(�$
��Left=�$��70��$��	10,10,0,0q�$	��Top=�$	��306���� -   $ 	groupBox1��$��	Filtering��$
��Left=�$	��725��$��	10,10,0,0q�$��1��$	��Top=�$	��306��$��2��.���� .+�	� -   	 	�Name$	 Median��
 	� 	GroupName$
 radios��$��Median Filter (Size only)��$��22��$	��Top=�	� -   $	 GaussianFilter��$
 radios��$��0,0,62,0q�$��Gaussian Filter��$��16��� $��77��.+��� $
��Size��$
��Left=�$��38��$��76���� -    ��Name$ GaussSize_Slide�� � Minimum$ 3Q� � Maximum$ 21Q�$
��Left=�$��31��$	��Top=�$	��164�� � Value �� *Text+ �� ElementName$ 	GaussSize���� -    ��Name$ 	GaussSize��$��Center=�$��26�� �� TextWrapping$
 Wrap=�$��3��$	��Top=�$��29���� $��Sigma��$��38��$��74���� -   $ GaussSigma_Slide��$	 0.5Q�$ 5Q�$
��Left=�$��31��$	��Top=�$	��164�� �� *Text+$ 
GaussSigma���� -   $ 
GaussSigma��$
��Left=�$��23��$
 Wrap=�$	��0.5��$	��Top=�$��29��� $
��Left=�$��
0,18,0,-12q�$	��284��$	��175��.+�	� -   $	 HiPass��$��HiPass��$
 radios��$
��Left=�$��31��$	��Top=�$��63��	� -   $	 Erode��$��Erode��$
 radios��$
��Left=�$��20��$	��Top=�$��63��	� -   $	 Invert��$��Invert��$
 radios��$
��Left=�$��23��$	��Top=�$��63��	� -   $	 EdgeDetector��$��Edge Detector��$
 radios��$
��Left=�$��22��$	��Top=�$	��117��	� -   $
	 More��$
��More��$
 radios��$
��Left=�$��25��$	��Top=�$	��117���� -    ��Name$ comboBox��{�. $
��Left=�$��23��$	��Top=�$	��117��.@��� -    ��Name$ Dilate��$��Dilate��� $��97��.+�	� -   $	 	Threshold�� d� 	IsChecked$
 TrueY�$
 radios��$��	Threshold��$
��Left=�$	��Top=�$	��100���� -   $ slider��$
��Left=�$��20�� � SmallChange$ 20Q� �� IsSnapToTickEnabled .  $	 255Q�$ 0Q�$	��109�� �� *Text+$ SliderValue���� -   $ SliderValue��{�. $
��Left=�$��22��$
 Wrap=�$��49������ *Value+$ slider���� -   $ Go��$��Apply��$
��Left=�$��66��$��
10,200,0,0q�$	��Top=�$	��306��$��2��