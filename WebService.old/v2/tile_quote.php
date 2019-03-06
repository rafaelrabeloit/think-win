<?php
	// Mensagens de Erro
	$msg[0] = "Conexão com o banco falhou!";
	$msg[1] = "Não foi possível selecionar o banco de dados!";

	$DeviceID = null;
	$Language = null;
	
	if ( isset($_GET['DeviceID']) ) $DeviceID = $_GET['DeviceID'];
	if ( isset($_GET['Language']) ) $Language = $_GET['Language'];
	

	if($DeviceID!=null && $Language != null){

		// Fazendo a conexão com o servidor MySQL
		$conexao = mysql_connect("localhost","rafaelrabelo","r2tvdr4r4w") or die($msg[0]);
		mysql_select_db("think_db",$conexao) or die($msg[1]);
		
		/*Seleciona o idioma*/
		$ididioma = 1;
		
		//retorna o maior id de Mensagens
		$query = "SELECT ididiomas FROM idiomas WHERE idioma='$Language' ";
		$resultadoLanguage = mysql_query($query,$conexao);
		
		if ($linhaLanguage = mysql_fetch_array($resultadoLanguage)) {
			$ididioma = $linhaLanguage['ididiomas'];
		}
		/*Fim da Seleção*/
		
		/*Seleciona o Dispositivo cadastrado */
		//Verifica o dispositivo cadastrado
		$query = "SELECT iddispositivos FROM dispositivos WHERE guid='$DeviceID'";
		$result = mysql_query($query,$conexao);
		
		$IdDevices = '0';
		
		if ( mysql_num_rows($result) > 0) {
			if( $row = mysql_fetch_array($result) ){
				$IdDevices = $row['iddispositivos'];
			}
		}
		else{
			$query = "INSERT INTO dispositivos (guid) VALUES ('$DeviceID')";
			$result = mysql_query($query,$conexao);
			
			$IdDevices = mysql_insert_id();
		}
		/*Fim da Seleção*/
		
		//retorna o maior id de Mensagens
		$MaxPermitido = -1;
		
		$query = "SELECT max(idmensagens) as max FROM mensagens WHERE idiomas_ididiomas=$ididioma";
		$resultadoMax = mysql_query($query,$conexao);
		
		if ($linhaMax = mysql_fetch_array($resultadoMax)) {
			$MaxPermitido = $linhaMax['max'];
			
			//Sorteia um valor qualquer entre 0 e o Máximo
			$LimiteInferior = rand ( 0 , $MaxPermitido );
			
			//Select que retorna a tupla com o maior valor proximo ao sorteado (sempre retornará pelo menos um valor
			$query = "SELECT idmensagens as sorteado, texto, autores.autor FROM mensagens INNER JOIN autores ON idautores = autores_idautores WHERE idmensagens >= $LimiteInferior AND aprovada=1 AND idiomas_ididiomas=$ididioma AND idmensagens NOT IN (SELECT mensagens_idmensagens FROM dispositivos_has_mensagens WHERE dispositivos_iddispositivos=$IdDevices ) LIMIT 1;";			
			$resultadoSorteado = mysql_query($query,$conexao);
			
			if ($linhaSorteado = mysql_fetch_array($resultadoSorteado)) {
				$IDMensagem = $linhaSorteado['sorteado'];
				$Autor = $linhaSorteado['autor'];
				$Quote = $linhaSorteado['texto'];		
			
				//Cadastra como lido :)
				$query = "INSERT INTO dispositivos_has_mensagens (dispositivos_iddispositivos, guid, mensagens_idmensagens, lido) VALUES ($IdDevices, '$DeviceID', $IDMensagem, 0)";				
				$result = mysql_query($query, $conexao);	
			}
			else{
				
				$query = "SELECT `texto_no_idioma` FROM `textos_in_idiomas` WHERE `ididiomas`=$ididioma AND (`idtextos` = 1 OR `idtextos` = 2) ORDER BY `idtextos` ASC";
				$resultadoMessage = mysql_query($query,$conexao);
			
				$IDMensagem = '-1';
				if ($linhaMessage = mysql_fetch_array($resultadoMessage)) {
					$Autor = $linhaMessage['texto_no_idioma'];
				}
				if ($linhaMessage = mysql_fetch_array($resultadoMessage)) {
					$Quote = $linhaMessage['texto_no_idioma'];
				}
			}
		}
		
		header ("Content-Type:text/xml");  
		
		echo "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
		echo "<tile>";
		echo  	"<visual>";
		echo		"<binding template=\"TileWideText09\">";
		echo	 		"<text id=\"1\">$Autor</text>";
		echo	  		"<text id=\"2\">$Quote</text>";
		echo		"</binding>";
		echo		"<binding template=\"TileSquareText02\">";
		echo	 		"<text id=\"1\">$Autor</text>";
		echo	  		"<text id=\"2\">$Quote</text>";
		echo		"</binding>";
		echo 	"</visual>";
		echo "</tile>";
		
		
		//fecha a conexÃ£o com o banco
		mysql_close($conexao);
	}
	else 
		echo "false";
		
?>
