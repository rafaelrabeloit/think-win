<?php
	// Mensagens de Erro
	$msg[0] = "Conexão com o banco falhou!";
	$msg[1] = "Não foi possível selecionar o banco de dados!";

	$DeviceID = null;

	if ( isset($_GET['DeviceID']) ) $DeviceID = $_GET['DeviceID'];
	

	if($DeviceID!=null){

		// Fazendo a conexão com o servidor MySQL
		$conexao = mysql_connect("localhost","rafaelrabelo","r2tvdr4r4w") or die($msg[0]);
		mysql_select_db("think_db",$conexao) or die($msg[1]);

		
		$querySelect = "SELECT mensagens.texto, autores.autor, grupos_in_idiomas.grupo_no_idioma, mensagens.idmensagens, dispositivos_has_mensagens.time
FROM mensagens
INNER JOIN dispositivos_has_mensagens ON mensagens_idmensagens = mensagens.idmensagens
INNER JOIN autores ON idautores = autores_idautores
INNER JOIN grupos_in_idiomas ON mensagens.grupos_idgrupos = grupos_in_idiomas.grupos_idgrupos
WHERE guid = '$DeviceID'
AND mensagens.idiomas_ididiomas = grupos_in_idiomas.idiomas_ididiomas
AND dispositivos_has_mensagens.lido =0";	
		$resultadoSelect = mysql_query($querySelect,$conexao);
			
		//marca todas como lidas
		$queryUpdate = "UPDATE dispositivos_has_mensagens SET lido=1 WHERE guid='$DeviceID' AND lido=0";
		$resultadoUpdate = mysql_query($queryUpdate,$conexao);
		
		//monta tabela com os resultados
		header ("Content-Type:text/xml");  
		echo "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
		echo 	"<quotes>";
		while ($linha = mysql_fetch_array($resultadoSelect)) {
			echo '<quote>';
			echo '<texto>'.$linha['texto'].'</texto>';
			echo '<autor>'.$linha['autor'].'</autor>';
			echo '<grupo>'.$linha['grupo_no_idioma'].'</grupo>';
			echo '<id>'.$linha['idmensagens'].'</id>';
			echo '<time>'.$linha['time'].'</time>';
			echo '</quote>';
		}
		echo 	"</quotes>";
		
		
		//fecha a conexÃ£o com o banco
		mysql_close($conexao);
	}
	else 
		echo "false";
?>
