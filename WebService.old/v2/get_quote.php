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
		$conexao = mysql_connect("localhost","rafael","r2tvdr4r4w") or die($msg[0]);
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
			
			//Select que retorna a tupla com o maior valor proximo ao sorteado (sempre retornará pelo menos um valor)
			$query = "SELECT idmensagens, texto, autores.autor, grupos_in_idiomas.grupo_no_idioma, grupos_in_idiomas.grupos_idgrupos
				FROM mensagens
				INNER JOIN autores ON idautores = autores_idautores
				INNER JOIN grupos_in_idiomas ON mensagens.grupos_idgrupos = grupos_in_idiomas.grupos_idgrupos
				WHERE idmensagens >= $LimiteInferior
				AND aprovada =1
				AND mensagens.idiomas_ididiomas =$ididioma
				AND grupos_in_idiomas.idiomas_ididiomas =$ididioma
				AND idmensagens NOT
				IN (
				SELECT mensagens_idmensagens
				FROM dispositivos_has_mensagens
				WHERE dispositivos_iddispositivos =$IdDevices
				)
				LIMIT 1";			
			$resultadoSorteado = mysql_query($query,$conexao);
			
			if ($linhaSorteado = mysql_fetch_array($resultadoSorteado)) {
				
				$IDMensagem = $linhaSorteado['idmensagens'];
				$Autor = $linhaSorteado['autor'];
				$Quote = $linhaSorteado['texto'];
				$Grupo = $linhaSorteado['grupo_no_idioma'];	
				$IDGrupo = $linhaSorteado['grupos_idgrupos'];			
			
				//Cadastra como lido :)
				$query = "INSERT INTO dispositivos_has_mensagens (dispositivos_iddispositivos, guid, mensagens_idmensagens, lido) VALUES ($IdDevices, '$DeviceID', $IDMensagem, 1)";				
				$result = mysql_query($query, $conexao);				
				$IdCadastrado = mysql_insert_id();
				
				$query = "SELECT time FROM dispositivos_has_mensagens WHERE iddispositivos_has_mensagens=$IdCadastrado";			
				$resultadoTempo = mysql_query($query,$conexao);
				
				if ($linhaTempo = mysql_fetch_array($resultadoTempo)) {
					$tempo = $linhaTempo['time'];
					
					//Abre inicio da tabela
					echo '<?xml version="1.0" encoding="utf-8"?>';
					echo '<quotes>';
					echo 	'<quote id="'.$IDMensagem.'" time="'.$tempo.'" autor="'.$Autor.'">';
					echo 		'<texto>'.$Quote.'</texto>';
					echo 		'<grupo id="'.$IDGrupo.'">'.$Grupo.'</grupo>';
					echo 	'</quote>';
					echo '</quotes>';
				}
			}
		}
		
		
		
		//fecha a conexÃ£o com o banco
		mysql_close($conexao);
	}
	else 
		echo "false";
		
?>
