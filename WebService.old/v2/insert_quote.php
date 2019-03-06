<html>
	<head>
		<meta charset='utf-8'> 
	</head>
	<body>
		<p>Inserindo Novas Citações Like a Boss</p>
		<?php
			// Mensagens de Erro
			$msg[0] = "Conexão com o banco falhou!";
			$msg[1] = "Não foi possível selecionar o banco de dados!";

			$quote_text = null;
			$language = null;
			$author = null;			
			$authorName = null;
			$group = null;
			$type = null;
			
			if ( isset($_POST['quote_text']) ) $quote_text = $_POST['quote_text'];
			if ( isset($_POST['language']) ) $language = $_POST['language'];
			if ( isset($_POST['author']) ) $author = $_POST['author'];
			if ( isset($_POST['authorName']) ) $authorName = $_POST['authorName'];
			if ( isset($_POST['group']) ) $group = $_POST['group'];
			if ( isset($_POST['type']) ) $type = $_POST['type'];
			

			if(	$quote_text != null &&
				$language != null &&
				$author != null &&
				$group != null &&
				$type != null ){

				// Fazendo a conexão com o servidor MySQL
				$conexao = mysql_connect("localhost","rafaelrabelo","r2tvdr4r4w") or die($msg[0]);
				mysql_select_db("think_db",$conexao) or die($msg[1]);
				
				//Cadastra o novo Autor
				if($author == 0){
					$query = "INSERT INTO `autores`(`autor`) VALUES (\"$authorName\")";				
					$result = mysql_query($query, $conexao);				
					$author = mysql_insert_id();
					
					echo "<b>Autor Incluida com Sucesso! $author</b> <br />";
				}
				
				$query = "INSERT INTO `mensagens`
				(`texto`, `aprovada`, `idiomas_ididiomas`, `grupos_idgrupos`, `autores_idautores`, `tipos_idtipos`) VALUES 
				(\"$quote_text\", 1,$language,$group,$author,$type)";				
				$result = mysql_query($query, $conexao);				
				$id_inserido = mysql_insert_id();
				
				echo "<b>Citação Incluida com Sucesso! $id_inserido</b><br />";
				
			}
			else
				echo "<b>Heey :) Faltou algo! </b><br />";
		?>
		
		<form method="post" action="insert_quote.php">
			Texto: <textarea rows="15" cols="20" name="quote_text" ></textarea><br />
			
			Idioma:<br />
			<select name="language">
			<?php
			
				$conexao = mysql_connect("localhost","rafaelrabelo","r2tvdr4r4w");
				mysql_select_db("think_db",$conexao);
				
				$query = "SELECT `ididiomas`, `idioma` FROM `idiomas`";
				$resultado = mysql_query($query,$conexao);
				
				while ($linha = mysql_fetch_array($resultado)) {
					$id = $linha['ididiomas'];
					$idioma = $linha['idioma'];
					
					echo '<option value="'.$id.'">'.$idioma.'</option>';
				}
			?>
			</select><br />
			
			Grupo:<br />			
			<select name="group" >
			<?php
			
				$conexao = mysql_connect("localhost","rafaelrabelo","r2tvdr4r4w");
				mysql_select_db("think_db",$conexao);
				
				$query = "SELECT `idgrupos`, `descricao` FROM `grupos`";
				$resultado = mysql_query($query,$conexao);
				
				while ($linha = mysql_fetch_array($resultado)) {
					$id = $linha['idgrupos'];
					$descricao = $linha['descricao'];
					
					echo '<option value="'.$id.'">'.$descricao.'</option>';
				}
			?>
			</select><br />
			
			Tipo:<br />			
			<select name="type">
			<?php
				$conexao = mysql_connect("localhost","rafaelrabelo","r2tvdr4r4w");
				mysql_select_db("think_db",$conexao);
				$query = "SELECT `idtipos`, `descricao` FROM `tipos`";
				$resultado = mysql_query($query,$conexao);
				
				while ($linha = mysql_fetch_array($resultado)) {
					$id = $linha['idtipos'];
					$descricao = $linha['descricao'];
					
					echo '<option value="'.$id.'">'.$descricao.'</option>';
				}
			?>
			</select><br />
			
			Autor:<br />			
			<select name="author">
			<option value="0">Novo Autor</option>
			<?php			
				$conexao = mysql_connect("localhost","rafaelrabelo","r2tvdr4r4w");
				mysql_select_db("think_db",$conexao);
				
				$query = "SELECT `idautores`, `autor` FROM `autores`";
				$resultado = mysql_query($query,$conexao);
				
				while ($linha = mysql_fetch_array($resultado)) {
					$id = $linha['idautores'];
					$autor = $linha['autor'];
					
					echo '<option value="'.$id.'">'.$autor.'</option>';
				}
			?>
			</select><br />
			
			New Autor Name:<input type="text" size="15" name="authorName"><br />
			
			<input type="submit" value="submit" name="submit"><br />
		</form>
		
		<?php 
			//fecha a conexÃ£o com o banco
			mysql_close($conexao);
		?>
	</body>
</html>