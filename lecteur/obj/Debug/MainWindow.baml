   M S B A M L   `   `   `  ����B  >lecteur, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:lecteurlecteur  5   	       lecteur.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   'localclr-namespace:lecteur     5   	     � Title$  Mon Lecteur��5   	   $��514.776��6   $��736.344��6.    J� 	AllowDrop . $	��525��6O   $	��350��6^   
���   �.��5      � ��5   
   �� 5       � ColumnDefinitions 5
   
   �� 5      $
��358* �6   �� 5      $��0* �6   �� 5      $��7* �6   .+�5   
   r� -   5         r�Name$ mediaElement1��5      $��114,35,12.6,85.8q�6.   $��3��6�   6�   �� 5   
   -   5         ��Name$
 Play��5      $��26,0,0,34.8q�6   $��Bottom=�64   $
��Left=�6d   $��49��6   $��42��6�   ��6�   Z� ��6�   Z� ��6�   Z� .��5      �� $��42��6   $��	play5.png��6    $
��Fill=�63   $��49��6B   5      �� 5   
   -   5        $
 Stop��5      $��139,0,0,34.8q�6   $��Bottom=�6L   $
��Left=�6g   $��52��6�   $��42��6�   ��6�   Z� ��6�   Z� ��6�   Z� .��5      �� $��42��6   $��	stop5.png��6    $
��Fill=�63   $��52��6B   5      �� 5   
   -   5         ��Name$ slider_seek��5   %   $��
10,0,9,9.8q�6    J� RenderTransformOrigin$ 0.498,0.777&�6�   $��20��6�   $��Bottom=�6�   �� 5   
   -   5        $ 
slider_vol��5   &   $��0,0,36,34.8q�6    � Maximum$ 1Q�6:    � Value$ 1Q�6F   $��17��6w   $��Bottom=�6�   $��Right=�6�   $��94��6�   �� 5   
   -   5        $ Pause��5      $��80,0,0,34.8q�6    $��Bottom=�6L   $
��Left=�6g   $��56��6�   $��42��6�   ��6�   Z� ��6�   Z� ��6�   Z� .��5      �� $��42��6   $��
pause5.png��6    $
��Fill=�64   $��49��6C   5      o� 5   
   $	��Top=�6   $��20��6'   $��3��63   $��
0,0,-0.4,0q�6G   .@�5      m� -   5        	 m�Name$
	 File��5      $��Média��6%   .@�5      m� -   5        $��Ouvrir��5      6+   m� 5      -	   5        $��Créer Playlist��5      $
��Left=�6K   $��139.2��6f   m� 5       -
   5        $��Quitter��5       6,   5!      m� 5"      -   5        $��Thèmes��5"      .@�5#      m� -   5        $��Defaut��5#      $
��Left=�6+   $��139.2��6F   6T   m� 5$      -   5        $��Ocean��5$      $
��Left=�6*   $��139.2��6E   6S   m� 5%      -   5        $��Pigeon��5%      $
��Left=�6+   $��139.2��6F   6T   m� 5&      -   5        $
��Ciel��5&      $
��Left=�6)   $��139.2��6D   6R   m� 5'      -   5        $
��Nuit��5'      $
��Left=�6)   $��139.2��6D   6R   5(      m� 5)      $
��Help��6   .@�5*      m� $��?��6   m� 5+      -   5        $��A propos de��5+      60   5,      5-      �� 5.   
   -   5        
 ��Name$
 mylist��5.   9   $
��Left=�6I   $��3,90,0,174.8q�6d   $	��100��6z   ���	#FF5A4646��6   ��  �� Path$ 	Playlists���5/      �� 50      .R�51      �� ��6    �� *	getname+6;   52      54      �� 55   
   -   5        $
 songlist��55   5   $
��Left=�6G   $	��261��6b   $��
108,25,0,0q�6o   $	��Top=�6�   $	��383��6�   $y�Hidden=�6�   ��6   �� $ Songs���56      �� 57      .R�58      �� ��6    �� *getnam+6:   59      5;      �� 5<   
   -   5         ��Name$ crtext��5<      $
��Left=�6"   $��25��6=   $��3,25,0,0q�6I    �� TextWrapping$
 Wrap=�6[   $��Newplaylist��6o   $	��Top=�6�   $��98��6�   $y�Hidden=�6�   �� 5=   
   -   5        $ Creer��5=      $
��Left=�6    $��	10,57,0,0q�6;   $	��Top=�6N   $��91��6f   $��40��6�   $y�Hidden=�6�   ��6�   Z� ��6�   Z� ��6�   Z� .��5>      �� $��34��6   $��
Creer5.png��6    $
��Fill=�64   $��68��6C   5?      �� 5@   
   -   5        $ saveplay��5@      $��
593,74,0,0q�6#   $	��Top=�67   $��37��6�   $
��Left=�6�   $��40��6�   $y�Hidden=�6�   ��6f   Z� ��6|   Z� ��6�   Z� .��5A      �� $��37��6   $��	save5.png��6    $
��Fill=�63   $��40��6B   5B      �� 5C   
   -   5        $
 muet��5C      $��Right=�6   $��0,0,135,28.8q�6;   $��49��6Q   $��42��6�   $��Bottom=�6�   $y�Hidden=�6�   ��6o   Z� ��6�   Z� ��6�   Z� .��5D      �� $��42��6   $��son_off5.png��6    $
��Fill=�66   $��49��6E   5E      �� 5F   
   -   5        $ resetvol��5F      $��0,0,138,31.8q�6#   $��44��6�   $��Bottom=�6�   $��Right=�6�   $��48��6�   ��6P   Z� ��6f   Z� ��6}   Z� .��5G      �� $��42��6   $��son_on5.png��6    $
��Fill=�65   $��49��6D   $ 0.5,0.5&�6O   z�5H      V� 5I      .~�5J      ��  �� ScaleY$ -1Q�6)   �� 5K      �� 5L      T� 5M      5N      5P      5Q      �� 5R   
   -   5        $ 
screenfull��5R      $��0,0,198,31.8q�6%   $��42��6�   $��Bottom=�6�   $��Right=�6�   $��49��6�   ��6;   Z� ��6Q   Z� ��6h   Z� .��5S      �� $��42��6   $��fullscreen5.png��6    $
��Fill=�69   $��49��6H   5T      �� 5U   
   -   5        $
 Prev��5U      $
��Left=�6   $��260,0,0,34.8q�6:   $��52��6P   $��42��6n   $��Bottom=�6z   ��6�   Z� ��6�   Z� ��6�   Z� .��5V      �� $��42��6   $��precedent5.png��6    $
��Fill=�68   $��52��6G   5W      �� 5X   
   -   5        $
 Next��5X      $��338,0,0,34.8q�6   $��42��6H   $��Bottom=�6T   $
��Left=�6�   $��52��6�   ��6o   Z� ��6�   Z� ��6�   Z� .��5Y      �� $��42��6   $��suivant5.png��6    $
��Fill=�66   $��52��6E   5Z      �� 5[   
   -   5         ��Name$ 	fondlabel��5[      $��40��6#   $��
524,22,0,0q�6/   $	��Top=�6C   $
��Left=�6q   $	��134��6�   $y�Hidden=�6�   ��6[   Z� .��5\      �� $��30��6   $��	Label.png��6    $
��Fill=�63   $	��128��6B   5]      �� 5^   
   -   5        $ 	titleplay��5^      $��Label��6#   $
��Left=�63   $��
557,29,0,0q�6N   $	��Top=�6b   $��25��6z   $	��133��6�   $y�Hidden=�6�   ��6�   Z� 6�   5_      5`      