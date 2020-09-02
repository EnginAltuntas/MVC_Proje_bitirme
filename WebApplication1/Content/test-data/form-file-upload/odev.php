<?php
if(isset($_FILES['dosya'])) {
   echo 'Dosya gönderilmiş';
} else {
   echo 'Lütfen bir dosya gönderin';
}

if(isset($_FILES['dosya'])){
   $hata = $_FILES['dosya']['error'];
   if($hata != 0) {
      echo 'Yüklenirken bir hata gerçekleşmiş.';
   } else {
      $boyut = $_FILES['dosya']['size'];
      if($boyut > (1024*1024*10)){
         echo 'Dosya 10MB den büyük olamaz.';
      } else {
         $tip = $_FILES['dosya']['type'];
         $isim = $_FILES['dosya']['name'];
         $uzanti = explode('.', $isim);
         $uzanti = $uzanti[count($uzanti)-1];
         if($tip != 'image/jpeg' || $uzanti != 'jpg' || $uzanti != 'txt') {
            echo 'Yanlızca JPG ve txt dosyaları gönderebilirsiniz.';
         } else {
            $dosya = $_FILES['dosya']['tmp_name'];
            copy($dosya, 'dosyalar/' . $_FILES['dosya']['name']);
            echo 'Dosyanız upload edildi!';
         }
      }
   }
}


?>

<?php
$ds = DIRECTORY_SEPARATOR;

$storeFolder = 'uploads';

if(!empty($_FILES)){

	$tempFile = $_FILES['file']['tmp_name'];

	$targetPath = dirname( __FILE__ ) . $ds. $storeFolder . $ds;

	$targetFile =  $targetPath. $_FILES['file']['name'];

	move_uploaded_file($tempFile,$targetFile);

}
?>