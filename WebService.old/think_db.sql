-- phpMyAdmin SQL Dump
-- version 3.5.2.2
-- http://www.phpmyadmin.net
--
-- Servidor: localhost
-- Tempo de Geração: 25/10/2012 às 02:03:30
-- Versão do Servidor: 5.5.19-log
-- Versão do PHP: 5.3.8

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Banco de Dados: `think_db`
--

-- --------------------------------------------------------

--
-- Estrutura da tabela `autores`
--

CREATE TABLE IF NOT EXISTS `autores` (
  `idautores` int(11) NOT NULL AUTO_INCREMENT,
  `autor` varchar(255) NOT NULL,
  PRIMARY KEY (`idautores`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=30 ;

--
-- Extraindo dados da tabela `autores`
--

INSERT INTO `autores` (`idautores`, `autor`) VALUES
(1, 'Mahatma Gandhi'),
(2, 'Gautama Buddha'),
(3, 'John Fitzgerald Kennedy'),
(4, 'Dr. Martin Luther King Jr'),
(5, 'Charles Darwin '),
(6, 'Albert Einstein '),
(7, 'Neil Gaiman'),
(8, 'Johann Sebastian Bach'),
(9, 'Isaac Asimov'),
(10, 'Francis Bacon'),
(11, 'Ralph Emerson'),
(12, 'Sócrates'),
(13, 'Carlos Drummond de Andrade'),
(14, 'Mohammad Mahdi Al-Jawahiri'),
(15, 'Woody Allen'),
(16, 'Isabel Allende'),
(17, 'Jane Austen'),
(18, 'Machado de Assis'),
(19, 'Isaac Asimov'),
(20, 'Arquimedes'),
(21, 'Aristóteles'),
(22, 'Dante Alighieri'),
(23, 'Roland Barthes'),
(24, 'Simone de Beauvoir'),
(25, 'Luís Vaz de Camões'),
(26, 'Lewis Carrol'),
(27, 'Charles Chaplin'),
(28, 'Agatha Christie'),
(29, 'Demócrito');

-- --------------------------------------------------------

--
-- Estrutura da tabela `dispositivos`
--

CREATE TABLE IF NOT EXISTS `dispositivos` (
  `iddispositivos` int(11) NOT NULL AUTO_INCREMENT,
  `guid` varchar(255) NOT NULL,
  PRIMARY KEY (`iddispositivos`),
  UNIQUE KEY `guid` (`guid`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=8746 ;
-- --------------------------------------------------------

--
-- Estrutura da tabela `dispositivos_has_mensagens`
--

CREATE TABLE IF NOT EXISTS `dispositivos_has_mensagens` (
  `iddispositivos_has_mensagens` int(11) NOT NULL AUTO_INCREMENT,
  `dispositivos_iddispositivos` int(11) NOT NULL,
  `mensagens_idmensagens` int(11) NOT NULL,
  `guid` varchar(255) NOT NULL,
  `lido` int(11) NOT NULL,
  `time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`iddispositivos_has_mensagens`),
  KEY `dispositivos_iddispositivos` (`dispositivos_iddispositivos`),
  KEY `mensagens_idmensagens` (`mensagens_idmensagens`),
  KEY `guid` (`guid`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=519411 ;

-- --------------------------------------------------------

--
-- Estrutura da tabela `grupos`
--

CREATE TABLE IF NOT EXISTS `grupos` (
  `idgrupos` int(11) NOT NULL AUTO_INCREMENT,
  `descricao` varchar(255) NOT NULL,
  PRIMARY KEY (`idgrupos`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=18 ;

--
-- Extraindo dados da tabela `grupos`
--

INSERT INTO `grupos` (`idgrupos`, `descricao`) VALUES
(1, 'Motivational'),
(2, 'Love'),
(3, 'Philosophy'),
(4, 'Humanity'),
(5, 'Religion'),
(6, 'Nature'),
(7, 'Political'),
(8, 'Peace'),
(9, 'War'),
(10, 'Education'),
(11, 'Money'),
(12, 'Trust'),
(13, 'Friendship'),
(14, 'Happiness'),
(15, 'Luck'),
(16, 'Time'),
(17, 'Wisdom');

-- --------------------------------------------------------

--
-- Estrutura da tabela `grupos_in_idiomas`
--

CREATE TABLE IF NOT EXISTS `grupos_in_idiomas` (
  `idgrupos_in_idiomas` int(11) NOT NULL AUTO_INCREMENT,
  `idiomas_ididiomas` int(11) NOT NULL,
  `grupos_idgrupos` int(11) NOT NULL,
  `grupo_no_idioma` varchar(255) NOT NULL,
  PRIMARY KEY (`idgrupos_in_idiomas`),
  KEY `idiomas_ididiomas` (`idiomas_ididiomas`,`grupos_idgrupos`),
  KEY `grupos_idgrupos` (`grupos_idgrupos`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=35 ;

--
-- Extraindo dados da tabela `grupos_in_idiomas`
--

INSERT INTO `grupos_in_idiomas` (`idgrupos_in_idiomas`, `idiomas_ididiomas`, `grupos_idgrupos`, `grupo_no_idioma`) VALUES
(1, 1, 1, 'Motivational'),
(2, 1, 2, 'Love'),
(3, 1, 3, 'Philosophy'),
(4, 1, 4, 'Humanity'),
(5, 1, 5, 'Religion'),
(6, 1, 6, 'Nature'),
(7, 1, 7, 'Political'),
(8, 1, 8, 'Peace'),
(9, 1, 9, 'War'),
(10, 1, 10, 'Education'),
(11, 1, 11, 'Money'),
(12, 1, 12, 'Trust'),
(13, 1, 13, 'Friendship'),
(14, 1, 14, 'Happiness'),
(15, 1, 15, 'Luck'),
(16, 1, 16, 'Time'),
(17, 1, 17, 'Wisdom'),
(18, 2, 1, 'Motivacional'),
(19, 2, 2, 'Ammor'),
(20, 2, 3, 'Filosofia'),
(21, 2, 4, 'Humanidade'),
(22, 2, 5, 'Religião'),
(23, 2, 6, 'Natureza'),
(24, 2, 7, 'Política'),
(25, 2, 8, 'Paz'),
(26, 2, 9, 'Guerra'),
(27, 2, 10, 'Educação'),
(28, 2, 11, 'Dinheiro'),
(29, 2, 12, 'Confiança'),
(30, 2, 13, 'Amizade'),
(31, 2, 14, 'Felicidade'),
(32, 2, 15, 'Sorte'),
(33, 2, 16, 'Tempo'),
(34, 2, 17, 'Sabedoria');

-- --------------------------------------------------------

--
-- Estrutura da tabela `idiomas`
--

CREATE TABLE IF NOT EXISTS `idiomas` (
  `ididiomas` int(11) NOT NULL AUTO_INCREMENT,
  `idioma` varchar(5) NOT NULL,
  PRIMARY KEY (`ididiomas`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=3 ;

--
-- Extraindo dados da tabela `idiomas`
--

INSERT INTO `idiomas` (`ididiomas`, `idioma`) VALUES
(1, 'en-US'),
(2, 'pt-BR');

-- --------------------------------------------------------

--
-- Estrutura da tabela `mensagens`
--

CREATE TABLE IF NOT EXISTS `mensagens` (
  `idmensagens` int(11) NOT NULL AUTO_INCREMENT,
  `texto` varchar(10240) DEFAULT NULL,
  `aprovada` tinyint(1) DEFAULT NULL,
  `idiomas_ididiomas` int(11) NOT NULL,
  `grupos_idgrupos` int(11) NOT NULL,
  `autores_idautores` int(11) NOT NULL,
  `tipos_idtipos` int(11) NOT NULL,
  PRIMARY KEY (`idmensagens`),
  KEY `idiomas_ididiomas` (`idiomas_ididiomas`),
  KEY `grupos_idgrupos` (`grupos_idgrupos`),
  KEY `autores_idautores` (`autores_idautores`),
  KEY `tipos_idtipos` (`tipos_idtipos`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=231 ;

--
-- Extraindo dados da tabela `mensagens`
--

INSERT INTO `mensagens` (`idmensagens`, `texto`, `aprovada`, `idiomas_ididiomas`, `grupos_idgrupos`, `autores_idautores`, `tipos_idtipos`) VALUES
(39, 'A man is but the product of his thoughts. What he thinks, he becomes.', 1, 1, 1, 1, 1),
(40, 'Victory attained by violence is tantamount to a defeat, for it is momentary. ', 1, 1, 1, 1, 1),
(41, 'Leo Tolstoy''s life has been devoted to replacing the method of violence for removing tyranny or securing reform by the method of non­resistance to evil. He would meet hatred expressed in violence by love expressed in self­suffering. He admits of no exception to whittle down this great and divine law of love. He applies it to all the problems that trouble mankind. ', 1, 1, 1, 1, 1),
(42, 'Nonviolence is the first article of my faith. It is also the last article of my creed. ', 1, 1, 1, 1, 1),
(43, 'Always believe in your dreams, because if you don''t, you''ll still have hope. ', 1, 1, 1, 1, 1),
(44, 'The only tyrant I accept in this world is the "still small voice" within me. And even though I have to face the prospect of being a minority of one, I humbly believe I have the courage to be in such a hopeless minority. ', 1, 1, 1, 1, 1),
(45, 'To call woman the weaker sex is a libel; it is man''s injustice to woman. If by strength is meant brute strength, then, indeed, is woman less brute than man. If by strength is meant moral power, then woman is immeasurably man''s superior. Has she not greater intuition, is she not more self-sacrificing, has she not greater powers of endurance, has she not greater courage? Without her, man could not be. If nonviolence is the law of our being, the future is with woman. Who can make a more effective appeal to the heart than woman? ', 1, 1, 1, 1, 1),
(46, 'Nothing is impossible for pure love. ', 1, 1, 2, 1, 1),
(48, 'Do not go by revelation; Do not go by tradition; Do not go by hearsay; Do not go on the authority of sacred texts; Do not go on the grounds of pure logic; Do not go by a view that seems rational; Do not go by reflecting on mere appearances; Do not go along with a considered view because you agree with it; Do not go along on the grounds that the person is competent; Do not go along because "the recluse is our teacher." Kalamas, when you yourselves know: These things are unwholesome, these things are blameworthy; these things are censured by the wise; and when undertaken and observed, these things lead to harm and ill, abandon them... Kalamas, when you know for yourselves: These are wholesome; these things are not blameworthy; these things are praised by the wise; undertaken and observed, these things lead to benefit and happiness, having undertaken them, abide in them. ', 1, 1, 3, 2, 1),
(49, 'Behold now, Bhikkhus, I exhort you: All compounded things are subject to decay. Strive with diligence! ', 1, 1, 3, 2, 1),
(51, 'But truly, Ananda, it is nothing strange that human beings should die. ', 1, 1, 4, 2, 1),
(52, 'Whatever is subject to origination is all subject to cessation. ', 1, 1, 3, 2, 1),
(53, 'This is deathless, the liberation of the mind through lack of clinging. ', 1, 1, 3, 2, 1),
(54, 'War will exist until that distant day when the conscientious objector enjoys the same reputation and prestige that the warrior does today. ', 1, 1, 9, 3, 1),
(55, 'You know my friends, there comes a time when people get tired of being trampled by the iron feet of oppression ... If we are wrong, the Supreme Court of this nation is wrong. If we are wrong, the Constitution of the United States is wrong. And if we are wrong, God Almighty is wrong. If we are wrong, Jesus of Nazareth was merely a utopian dreamer that never came down to Earth. If we are wrong, justice is a lie, love has no meaning. And we are determined here in Montgomery to work and fight until "justice runs down like water, and righteousness like a mighty stream." ', 1, 1, 7, 4, 1),
(56, 'True peace is not merely the absence of tension: it is the presence of justice. ', 1, 1, 8, 4, 1),
(57, 'Men often hate each other because they fear each other; they fear each other because they don''t know each other; they don''t know each other because they can not communicate; they can not communicate because they are separated. ', 1, 1, 7, 4, 1),
(58, 'Man is man because he is free to operate within the framework of his destiny. He is free to deliberate, to make decisions, and to choose between alternatives. He is distinguished from animals by his freedom to do evil or to do good and to walk the high road of beauty or tread the low road of ugly degeneracy. ', 1, 1, 4, 4, 1),
(59, 'I submit to you that if a man has not discovered something that he will die for, he isn''t fit to live. ', 1, 1, 4, 4, 1),
(60, 'There are certain things in our nation and in the world which I am proud to be maladjusted and which I hope all men of good-will will be maladjusted until the good societies realize — I say very honestly that I never intend to become adjusted to — segregation and discrimination. I never intend to become adjusted to religious bigotry. I never intend to adjust myself to economic conditions that will take necessities from the many to give luxuries to the few. I never intend to adjust myself to the madness of militarism, to self-defeating effects of physical violence. But in a day when sputniks and explorers are dashing through outer space and guided ballistic missiles are carving highways of death through the stratosphere, no nation can win a war. It is no longer the choice between violence and nonviolence. It is either nonviolence or nonexistence… ', 1, 1, 4, 4, 1),
(61, 'I must admit that I have gone through those moments when I was greatly disappointed with the church and what it has done in this period of social change. We must face the fact that in America, the church is still the most segregated major institution in America. At 11:00 on Sunday morning when we stand and sing and Christ has no east or west, we stand at the most segregated hour in this nation. This is tragic. Nobody of honesty can overlook this. Now, I''m sure that if the church had taken a stronger stand all along, we wouldn''t have many of the problems that we have. The first way that the church can repent, the first way that it can move out into the arena of social reform is to remove the yoke of segregation from its own body. Now, I''m not saying that society must sit down and wait on a spiritual and moribund church as we''ve so often seen. I think it should have started in the church, but since it didn''t start in the church, our society needed to move on. The church, itself, will stand under the judgement of God. Now that the mistake of the past has been made, I think that the opportunity of the future is to really go out and to transform American society, and where else is there a better place than in the institution that should serve as the moral guardian of the community. The institution that should preach brotherhood and make it a reality within its own body. ', 1, 1, 7, 4, 1),
(62, ' A riot is the language of the unheard.', 1, 1, 7, 4, 1),
(63, 'There is no fundamental difference between man and the higher animals in their mental faculties. ', 1, 1, 6, 5, 1),
(64, 'As for a future life, every man must judge for himself between conflicting vague probabilities. ', 1, 1, 4, 5, 1),
(65, 'A man''s friendships are one of the best measures of his worth', 1, 1, 4, 5, 1),
(66, 'Why is it nobody understands me and everybody likes me? ', 1, 1, 3, 6, 1),
(67, 'Perfection of means and confusion of goals seem—in my opinion—to characterize our age. ', 1, 1, 3, 6, 1),
(68, 'The eternal mystery of the world is its comprehensibility ... The fact that it is comprehensible is a miracle. ', 1, 1, 3, 6, 1),
(69, 'All of science is nothing more than the refinement of everyday thinking. ', 1, 1, 6, 6, 1),
(70, 'Make things as simple as possible, but not simpler ', 1, 1, 3, 6, 1),
(71, 'I see a clock, but I cannot envision the clockmaker. The human mind is unable to conceive of the four dimensions, so how can it conceive of a God, before whom a thousand years and a thousand dimensions are as one ? ', 1, 1, 3, 6, 1),
(72, 'I am not only a pacifist but a militant pacifist. I am willing to fight for peace. Nothing will end war unless the people themselves refuse to go to war. ', 1, 1, 8, 6, 1),
(73, 'It is my view that a vegetarian manner of living by its purely physical effect on the human temperament would most beneficially influence the lot of mankind. ', 1, 1, 3, 6, 1),
(74, 'Peace cannot be kept by force. It can only be achieved by understanding. You cannot subjugate a nation forcibly unless you wipe out every man, woman, and child. Unless you wish to use such drastic measures, you must find a way of settling your disputes without resort to arms. ', 1, 1, 8, 6, 1),
(75, 'I believe that whatever we do or live for has its causality; it is good, however, that we cannot see through to it. ', 1, 1, 3, 6, 1),
(76, 'Life is like riding a bicycle. To keep your balance you must keep moving. ', 1, 1, 1, 6, 1),
(77, 'Make a lot of walks to get healthy and don’t read that much but save yourself some until you’re grown up. ', 1, 1, 1, 6, 1),
(78, 'Nature shows us only the tail of the lion. But there is no doubt in my mind that the lion belongs with it even if he cannot reveal himself to the eye all at once because of his huge dimension. ', 1, 1, 6, 6, 1),
(80, 'We are small but we are many, we are many we are small; we were here before you rose, we will be here when you fall. ', 1, 1, 3, 7, 1),
(81, 'You get ideas from daydreaming. You get ideas from being bored. You get ideas all the time. The only difference between writers and other people is we notice when we''re doing it. ', 1, 1, 1, 7, 1),
(82, 'You get ideas all the time. The only difference between writers and other people is we notice when we''re doing it. ', 1, 1, 1, 7, 1),
(83, 'Everybody has a secret world inside of them. All of the people of the world, I mean everybody. No matter how dull and boring they are on the outside, inside them they''ve all got unimaginable, magnificent, wonderful, stupid, amazing worlds. Not just one world. Hundreds of them. Thousands maybe. ', 1, 1, 1, 7, 1),
(84, 'To the glory of the most high God, and that my neighbour may be benefited thereby. ', 1, 1, 5, 8, 1),
(85, 'Individual science fiction stories may seem as trivial as ever to the blinder critics and philosophers of today — but the core of science fiction, its essence, the concept around which it revolves, has become crucial to our salvation if we are to be saved at all. ', 1, 1, 4, 19, 1),
(86, 'People are entirely too disbelieving of coincidence. They are far too ready to dismiss it and to build arcane structures of extremely rickety substance in order to avoid it. I, on the other hand, see coincidence everywhere as an inevitable consequence of the laws of probability, according to which having no unusual coincidence is far more unusual than any coincidence could possibly be. ', 1, 1, 4, 19, 1),
(87, 'There is no belief, however foolish, that will not gather its faithful adherents who will defend it to the death. ', 1, 1, 4, 19, 1),
(88, 'Outside intelligences, exploring the Solar System with true impartiality, would be quite likely to enter the Sun in their records thus: Star X, spectral class G0, 4 planets plus debris. ', 1, 1, 4, 19, 1),
(89, ' The monuments of wit survive the monuments of power.', 1, 1, 7, 10, 1),
(90, ' Nothing is terrible except fear itself.', 1, 1, 1, 10, 1),
(91, ' Lucid intervals and happy pauses.', 1, 1, 1, 10, 1),
(92, 'I confess that I have as vast contemplative ends, as I have moderate civil ends: for I have taken all knowledge to be my province; and if I could purge it of two sorts of rovers, whereof the one with frivolous disputations, confutations, and verbosities, the other with blind experiments and auricular traditions and impostures, hath committed so many spoils, I hope I should bring in industrious observations, grounded conclusions, and profitable inventions and discoveries; the best state of that province. This, whether it be curiosity, or vain glory, or nature, or (if one take it favourably) philanthropia, is so fixed in my mind as it cannot be removed. And I do easily see, that place of any reasonable countenance doth bring commandment of more wits than of a man''s own; which is the thing I greatly affect. ', 1, 1, 7, 10, 1),
(93, ' Knowledge is power.', 1, 1, 4, 10, 1),
(94, ' Riches are a good handmaid, but the worst mistress.', 1, 1, 4, 10, 1),
(95, 'Nenhum grande homem se queixa de falta de oportunidades.', 1, 2, 1, 11, 1),
(96, 'Aquele a quem a palavra não educar, também o pau não educará.', 1, 2, 3, 12, 1),
(97, 'Os homens são como as moedas; devemos tomá-los pelo seu valor, seja qual for o seu cunho.', 1, 2, 3, 13, 1),
(98, 'Necessitamos sempre de ambicionar alguma coisa que, alcançada, não nos torna sem ambição.', 1, 2, 1, 13, 1),
(99, 'O cofre do banco contém apenas dinheiro. Frustar-se-á quem pensar que nele encontrará riqueza.', 1, 2, 11, 13, 1),
(100, 'A educação visa melhorar a natureza do homem o que nem sempre é aceite pelo interessado.', 1, 2, 10, 13, 1),
(101, 'As obras-primas devem ter sido geradas por acaso; a produção voluntária não vai além da mediocridade.', 1, 2, 3, 13, 1),
(102, 'O cofre do banco contém apenas dinheiro. Frustar-se-á quem pensar que nele encontrará riqueza.', 1, 2, 11, 13, 1),
(103, 'A educação visa melhorar a natureza do homem o que nem sempre é aceite pelo interessado.', 1, 2, 10, 13, 1),
(104, 'As obras-primas devem ter sido geradas por acaso; a produção voluntária não vai além da mediocridade.', 1, 2, 3, 13, 1),
(105, 'Como as plantas a amizade não deve ser muito nem pouco regada.', 1, 2, 13, 13, 1),
(106, 'Há vários motivos para não amar uma pessoa, e um só para amá-la; este prevalece.', 1, 2, 2, 13, 1),
(107, 'Perder tempo em aprender coisas que não interessam, priva-nos de descobrir coisas interessantes.', 1, 2, 10, 13, 1),
(108, 'A educação para o sofrimento, evitaria senti-lo, em relação a casos que não o merecem.', 1, 2, 10, 13, 1),
(109, 'A amizade é um meio de nos isolarmos da humanidade cultivando algumas pessoas.', 1, 2, 13, 13, 1),
(110, 'A confiança é um ato de fé, e esta dispensa raciocínio.', 1, 2, 12, 13, 1),
(111, 'A leitura é uma fonte inesgotável de prazer mas por incrível que pareça, a quase totalidade, não sente esta sede.', 1, 2, 10, 13, 1),
(112, 'As dificuldades são o aço estrutural que entra na construção do carácter.', 1, 2, 4, 13, 1),
(113, 'Há duas épocas na vida, infância e velhice, em que a felicidade está numa caixa de bombons.', 1, 2, 4, 13, 1),
(114, 'Ser feliz sem motivo é a mais autêntica forma de felicidade.', 1, 2, 14, 13, 1),
(115, 'Só é lutador quem sabe lutar consigo mesmo.', 1, 2, 1, 13, 1),
(116, 'Ninguém é igual a ninguém. Todo o ser humano é um estranho ímpar.', 1, 2, 4, 13, 1),
(117, 'Não é fácil ter paciência diante dos que têm excesso de paciência.', 1, 2, 3, 13, 1),
(118, 'A minha vontade é forte, mas a minha disposição em lhe obedecer é fraca.', 1, 2, 9, 13, 1),
(119, 'Acontece com os homens o mesmo que se passa com as paixões: há as nobres e as outras.', 1, 2, 4, 14, 1),
(120, 'O homem explora o homem e por vezes é o contrário.', 1, 2, 4, 15, 1),
(121, 'A vocação de um político de carreira é fazer de cada solução um problema.', 1, 2, 7, 15, 1),
(122, 'Aquele que procura a verdade corre o risco de a encontrar.', 1, 2, 15, 16, 1),
(123, 'A guerra é a obra de arte dos militares, a coroação da sua formação, a insígnia dourada da sua profissão. Não foram criados para brilhar na paz.', 1, 2, 9, 16, 1),
(124, 'A tortura é uma experiência humilhante. A meta não é obter informação, mas castigar-nos e destroçar-nos tanto, que façamos o que as autoridades querem. Transformamo-nos num exemplo para os outros, que ficam aterrorizados para sempre.', 1, 2, 9, 16, 1),
(125, 'A relação sexual dá essa intimidade que somente a mãe e o recém-nascido têm.', 1, 2, 2, 16, 1),
(126, 'O negócio pode trazer dinheiro, mas a amizade raramente o faz.', 1, 2, 11, 17, 1),
(127, 'Há casos em que um conselho pode ser tanto bom quanto mau - dependerá dos acontecimentos', 1, 2, 13, 17, 1),
(128, 'É uma verdade universalmente aceita que um homem solteiro, dotado de uma certa fortuna, precisa de uma esposa.', 1, 2, 14, 17, 1),
(129, 'A vaidade e o orgulho são coisas diferentes, embora as palavras sejam frequentemente usadas como sinónimos. Uma pessoa pode ser orgulhosa sem ser vaidosa. O orgulho relaciona-se mais com a opinião que temos de nós mesmos, e a vaidade, com o que desejaríamos que os outros pensassem de nós.', 1, 2, 4, 17, 1),
(130, 'Não se ama duas vezes a mesma mulher.', 1, 2, 2, 18, 1),
(131, 'O amor é o egoísmo duplicado.', 1, 2, 2, 18, 1),
(132, 'Lágrimas não são argumentos.', 1, 2, 1, 18, 1),
(133, 'Está morto: podemos elogiá-lo à vontade.', 1, 2, 7, 18, 1),
(134, 'Eu não sou homem que recuse elogios. Amo-os; eles fazem bem à alma e até ao corpo. As melhores digestões da minha vida são as dos jantares em que sou brindado.', 1, 2, 14, 18, 1),
(135, 'O amor é o rei dos moços e o tirano dos velhos.', 1, 2, 2, 18, 1),
(136, 'Não importa ao tempo o minuto que passa, mas o minuto que vem.', 1, 2, 3, 18, 1),
(137, 'Importuna coisa é a felicidade alheia quando somos vítima de algum infortúnio.', 1, 2, 14, 18, 1),
(138, 'O tempo, que a tradição mitológica nos pinta com alvas barbas, é, pelo contrário, um eterno rapagão; só parece velho àqueles que já o estão; em si mesmo traz a perpétua e versátil juventude.', 1, 2, 16, 18, 1),
(139, 'Escrever é uma questão de colocar acentos.', 1, 2, 10, 18, 1),
(140, 'O coração é a região do inesperado.', 1, 2, 2, 18, 1),
(141, 'As melhores mulheres pertencem aos homens mais atrevidos.', 1, 2, 2, 18, 1),
(142, 'Não levante a espada sobre a cabeça de quem te pediu perdão.', 1, 2, 8, 18, 1),
(143, 'A arte de viver consiste em tirar o maior bem do maior mal.', 1, 2, 3, 18, 1),
(144, 'Atrás de toda a ação, há sempre uma intenção.', 1, 2, 4, 18, 1),
(145, 'O tempo é um químico invisível, que dissolve, compõe, extrai e transforma todas as substâncias morais.', 1, 2, 16, 18, 1),
(146, 'Um cientista é um homem tão frágil e humano como qualquer outro; contudo, a busca científica pode enobrecê-lo, inclusivamente contra a sua vontade.', 1, 2, 10, 9, 1),
(147, 'Um sutil pensamento erróneo pode dar lugar a uma indagação frutífera que revela verdades de grande valor.', 1, 2, 10, 9, 1),
(148, 'O aspecto mais triste da vida atual é que a ciência ganha em conhecimento mais rapidamente que a sociedade em sabedoria.', 1, 2, 4, 9, 1),
(149, 'A violência é o último refúgio do incompetente.', 1, 2, 9, 9, 1),
(150, 'A espécie humana apenas se pode permitir uma guerra: a guerra contra a sua própria extinção.', 1, 2, 9, 9, 1),
(151, 'Os escritores de ficção científica prevêem o inevitável e, ainda que os problemas e as catástrofes possam ser evitáveis, não há soluções.', 1, 2, 16, 9, 1),
(152, 'A primeira lei dos nutricionistas parece ser esta: se sabe bem, faz-te mal.', 1, 2, 14, 9, 1),
(153, 'A Terra retrocedeu. A Humanidade retrocedeu em todos os lados exceto na Lua.', 1, 2, 4, 9, 1),
(154, 'Se o conhecimento pode criar problemas, não será através da ignorância que os resolveremos.', 1, 2, 10, 9, 1),
(155, 'Quem tiver talento, obterá o êxito na medida que lhe corresponda. Porém, apenas se persistir naquilo que faz.', 1, 2, 10, 9, 1),
(156, 'A vida é jovial. A morte é a calma. É a transição entre uma e a outra que nos cria problemas.', 1, 2, 16, 9, 1),
(157, 'Dai-me um ponto de apoio e levantarei o mundo.', 1, 2, 3, 20, 1),
(158, 'Sê senhor da tua vontade e escravo da tua consciência.', 1, 2, 3, 21, 1),
(159, 'O homem prudente não diz tudo quanto pensa, mas pensa tudo quanto diz.', 1, 2, 17, 21, 1),
(160, 'Ter muitos amigos é não ter nenhum', 1, 2, 13, 21, 1),
(161, 'A amizade é uma alma com dois corpos.', 1, 2, 13, 21, 1),
(162, 'A arte é a ideia da obra, a ideia que existe sem matéria.', 1, 2, 16, 21, 1),
(163, 'Os avarentos amealham como se fossem viver para sempre, os pródigos dissipam, como se fossem morrer.', 1, 2, 16, 21, 1),
(164, 'O belo é o esplendor da ordem.', 1, 2, 17, 21, 1),
(165, 'A dúvida é o principio da sabedoria.', 1, 2, 17, 21, 1),
(166, 'O egoísmo não é amor por nós próprios, mas uma desvairada paixão por nós próprios.', 1, 2, 2, 21, 1),
(167, 'A primeira qualidade do estilo é a clareza.', 1, 2, 3, 21, 1),
(168, 'O objetivo da guerra é a Paz.', 1, 2, 8, 21, 1),
(169, 'O ignorante afirma, o sábio duvida, o sensato reflete.', 1, 2, 17, 21, 1),
(170, 'Haverá flagelo mais terrível do que a injustiça de armas na mão?', 1, 2, 9, 21, 1),
(171, 'É ótimo não se exercer qualquer profissão, pois um homem livre não deve viver para servir outro.', 1, 2, 14, 21, 1),
(172, 'O reconhecimento envelhece depressa', 1, 2, 16, 21, 1),
(173, 'A educação tem raízes amargas, mas os seus frutos são doces.', 1, 2, 10, 21, 1),
(174, 'A música é celeste, de natureza divina e de tal beleza que encanta a alma e a eleva acima da sua condição.', 1, 2, 6, 21, 1),
(175, 'O prazer no trabalho aperfeiçoa a obra.', 1, 2, 14, 21, 1),
(176, 'Devemos comportar-nos com os nossos amigos do mesmo modo que gostaríamos que eles se comportassem conosco.', 1, 2, 13, 21, 1),
(177, 'No fundo de um buraco ou de um poço, acontece descobrir-se as estrelas.', 1, 2, 15, 21, 1),
(178, 'Todos os trabalhos pagos absorvem e degradam o espírito.', 1, 2, 4, 21, 1),
(179, 'A felicidade não se encontra nos bens exteriores.', 1, 2, 14, 21, 1),
(180, 'A alegria que se tem em pensar e aprender faz-nos pensar e aprender ainda mais.', 1, 2, 10, 21, 1),
(181, 'O livro é um animal vivo.', 1, 2, 10, 21, 1),
(182, 'A mudança em todas as coisas é desejável.', 1, 2, 16, 21, 1),
(183, 'Aquele que nunca aprendeu a obedecer não pode ser um bom comandante.', 1, 2, 7, 21, 1),
(184, 'Sendo assim, as revoluções não concernem a pequenas questões, mas nascem de pequenas questões e põem em jogo grandes questões.', 1, 2, 7, 21, 1),
(185, 'Aquele que mais sabe, mais lastima o tempo perdido.', 1, 2, 16, 22, 1),
(186, 'Não há comparação entre o que se perde por fracassar e o que se perde por não tentar.', 1, 2, 17, 10, 1),
(187, 'Não há solidão mais triste do que a do homem sem amizades. A falta de amigos faz com que o mundo pareça um deserto.', 1, 2, 13, 10, 1),
(188, 'Instrução e capacidade humana, são sinónimos.', 1, 2, 10, 10, 1),
(189, 'No que respeita à caridade nunca se pode pecar por excesso.', 1, 2, 4, 10, 1),
(190, 'Não há beleza perfeita que não contenha algo de estranho nas suas proporções.', 1, 2, 6, 10, 1),
(191, 'O conhecimento é em si mesmo um poder.', 1, 2, 10, 10, 1),
(192, 'Como ciumento sofro quatro vezes: por ser excluído, por ser agressivo, por ser doido e por ser vulgar.', 1, 2, 2, 23, 1),
(193, 'O fascismo não é impedir-nos de dizer, é obrigar-nos a dizer.', 1, 2, 7, 23, 1),
(194, 'É pelo trabalho que a mulher vem diminuindo a distância que a separava do homem, somente o trabalho poderá garantir-lhe uma independência concreta.', 1, 2, 10, 24, 1),
(195, 'É horrível assistir à agonia de uma esperança.', 1, 2, 9, 24, 1),
(196, 'O que é um adulto ? Uma criança de idade.', 1, 2, 4, 24, 1),
(197, 'O compromisso multiplica por dois as obrigações familiares e todos os compromissos sociais.', 1, 2, 4, 24, 1),
(198, 'É na arte que o homem se ultrapassa definitivamente.', 1, 2, 17, 24, 1),
(199, 'Coisas impossíveis, é melhor esquecê-las que desejá-las.', 1, 2, 17, 25, 1),
(200, 'É fraqueza entre ovelhas ser leão.', 1, 2, 6, 25, 1),
(201, 'Um baixo amor os fortes enfraquece.', 1, 2, 2, 25, 1),
(202, 'Quem não sabe a arte, não a estima.', 1, 2, 10, 25, 1),
(203, 'Quem quis, sempre pôde.', 1, 2, 4, 25, 1),
(204, 'Pobre memória, que só anda de marcha atrás.', 1, 2, 16, 26, 1),
(205, 'Se todos se ocupassem do que lhes diz respeito, o mundo andaria mais depressa.', 1, 2, 10, 26, 1),
(206, 'Tudo tem uma moral, se a encontrarmos.', 1, 2, 17, 26, 1),
(207, 'Somos tão pacientes connosco mesmos, que nunca nos irritamos com a nossa própria estupidez.', 1, 2, 9, 26, 1),
(208, 'O que te contar três vezes é verdade.', 1, 2, 17, 26, 1),
(209, 'As pessoas podem duvidar do que dizes, mas acreditarão no que fizeres.', 1, 2, 1, 26, 1),
(210, 'A cortesia é pensar o que dizes. Poupa muito tempo.', 1, 2, 10, 26, 1),
(211, 'Creio no riso e nas lágrimas como antídotos contra o ódio e o terror.', 1, 2, 8, 27, 1),
(212, 'Num filme o que importa não é a realidade, mas o que dela possa extrair a imaginação.', 1, 2, 3, 27, 1),
(213, 'O assunto mais importante do mundo pode ser simplificado até ao ponto em que todos possam apreciá-lo e compreendê-lo. Isso é - ou deveria ser - a mais elevada forma de arte.', 1, 2, 10, 27, 1),
(214, 'A beleza existe em tudo - tanto no bem como no mal. Mas somente os artistas e os poetas sabem encontrá-la.', 1, 2, 17, 27, 1),
(215, 'A persistência é o caminho do êxito.', 1, 2, 1, 27, 1),
(216, 'O amor perfeito é a mais bela das frustrações, pois está acima do que se pode exprimir.', 1, 2, 2, 27, 1),
(217, 'A humanidade não se divide em heróis e tiranos. As suas paixões, boas e más, foram-lhe dadas pela sociedade, não pela natureza.', 1, 2, 4, 27, 1),
(218, 'Conhecer o homem - esta é a base de todo o sucesso.', 1, 2, 4, 27, 1),
(219, 'As conversas são sempre perigosas quando se tem algo a ocultar.', 1, 2, 17, 28, 1),
(220, 'Os velhos pecados têm sombras grandes.', 1, 2, 16, 28, 1),
(221, 'No que diz respeito às grandes somas, o mais recomendável é não confiar em ninguém.', 1, 2, 11, 28, 1),
(222, 'Ganhar uma guerra é tão desastroso como perdê-la.', 1, 2, 9, 28, 1),
(223, 'O assassino costuma ser um antigo amigo da vítima.', 1, 2, 13, 28, 1),
(224, 'A melhor receita para o romance policial: o detective não deve saber nunca mais do que o leitor.', 1, 2, 3, 28, 1),
(225, 'Qualquer mulher pode enganar um homem, se ela quiser e ele estiver apaixonado por ela.', 1, 2, 2, 28, 1),
(226, 'Nada mais curioso do que os hábitos. Quase ninguém sabe que os tem.', 1, 2, 4, 28, 1),
(227, 'Não reconhecemos os momentos realmente importantes da vida até ser demasiado tarde.', 1, 2, 16, 28, 1),
(228, 'O animal é tão ou mais sábio do que o homem: conhece a medida da sua necessidade, enquanto o homem a ignora.', 1, 2, 4, 29, 1),
(229, 'Os homens, ao fugir da morte, perseguem-na.', 1, 2, 4, 29, 1),
(230, 'O homem é um microcosmo.', 1, 2, 4, 29, 1);

-- --------------------------------------------------------

--
-- Estrutura da tabela `textos`
--

CREATE TABLE IF NOT EXISTS `textos` (
  `idtextos` int(11) NOT NULL AUTO_INCREMENT,
  `descricao` varchar(255) NOT NULL,
  PRIMARY KEY (`idtextos`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=3 ;

--
-- Extraindo dados da tabela `textos`
--

INSERT INTO `textos` (`idtextos`, `descricao`) VALUES
(1, 'NoMoreQuotesTitle'),
(2, 'NoMoreQuotesDescription');

-- --------------------------------------------------------

--
-- Estrutura da tabela `textos_in_idiomas`
--

CREATE TABLE IF NOT EXISTS `textos_in_idiomas` (
  `idtextos_in_idiomas` int(11) NOT NULL AUTO_INCREMENT,
  `textos_idtextos` int(11) NOT NULL,
  `idiomas_ididiomas` int(11) NOT NULL,
  `texto_no_idioma` varchar(1024) NOT NULL,
  PRIMARY KEY (`idtextos_in_idiomas`),
  KEY `textos_idtextos` (`textos_idtextos`),
  KEY `idiomas_ididiomas` (`idiomas_ididiomas`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=5 ;

--
-- Extraindo dados da tabela `textos_in_idiomas`
--

INSERT INTO `textos_in_idiomas` (`idtextos_in_idiomas`, `textos_idtextos`, `idiomas_ididiomas`, `texto_no_idioma`) VALUES
(1, 1, 1, 'Oh No!'),
(2, 1, 2, 'Ah Não!'),
(3, 2, 1, 'We have no more quotes for you for now.'),
(4, 2, 2, 'Nós não temos mais citações para você por enquanto.');

-- --------------------------------------------------------

--
-- Estrutura da tabela `tipos`
--

CREATE TABLE IF NOT EXISTS `tipos` (
  `idtipos` int(11) NOT NULL AUTO_INCREMENT,
  `descricao` varchar(127) NOT NULL,
  PRIMARY KEY (`idtipos`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=3 ;

--
-- Extraindo dados da tabela `tipos`
--

INSERT INTO `tipos` (`idtipos`, `descricao`) VALUES
(1, 'Book'),
(2, 'Music');

-- --------------------------------------------------------

--
-- Estrutura da tabela `tipos_in_idiomas`
--

CREATE TABLE IF NOT EXISTS `tipos_in_idiomas` (
  `idtipos_in_idiomas` int(11) NOT NULL AUTO_INCREMENT,
  `idiomas_ididiomas` int(11) NOT NULL,
  `tipos_idtipos` int(11) NOT NULL,
  `tipo_no_idioma` varchar(127) NOT NULL,
  PRIMARY KEY (`idtipos_in_idiomas`),
  KEY `idiomas_ididiomas` (`idiomas_ididiomas`),
  KEY `tipos_idtipos` (`tipos_idtipos`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=3 ;

--
-- Extraindo dados da tabela `tipos_in_idiomas`
--

INSERT INTO `tipos_in_idiomas` (`idtipos_in_idiomas`, `idiomas_ididiomas`, `tipos_idtipos`, `tipo_no_idioma`) VALUES
(1, 1, 1, 'Books'),
(2, 1, 2, 'Musics');

--
-- Restrições para as tabelas dumpadas
--

--
-- Restrições para a tabela `dispositivos_has_mensagens`
--
ALTER TABLE `dispositivos_has_mensagens`
  ADD CONSTRAINT `dispositivos_has_mensagens_ibfk_1` FOREIGN KEY (`dispositivos_iddispositivos`) REFERENCES `dispositivos` (`iddispositivos`),
  ADD CONSTRAINT `dispositivos_has_mensagens_ibfk_2` FOREIGN KEY (`mensagens_idmensagens`) REFERENCES `mensagens` (`idmensagens`),
  ADD CONSTRAINT `dispositivos_has_mensagens_ibfk_3` FOREIGN KEY (`guid`) REFERENCES `dispositivos` (`guid`);

--
-- Restrições para a tabela `grupos_in_idiomas`
--
ALTER TABLE `grupos_in_idiomas`
  ADD CONSTRAINT `grupos_in_idiomas_ibfk_1` FOREIGN KEY (`idiomas_ididiomas`) REFERENCES `idiomas` (`ididiomas`),
  ADD CONSTRAINT `grupos_in_idiomas_ibfk_2` FOREIGN KEY (`grupos_idgrupos`) REFERENCES `grupos` (`idgrupos`);

--
-- Restrições para a tabela `mensagens`
--
ALTER TABLE `mensagens`
  ADD CONSTRAINT `mensagens_ibfk_1` FOREIGN KEY (`idiomas_ididiomas`) REFERENCES `idiomas` (`ididiomas`),
  ADD CONSTRAINT `mensagens_ibfk_2` FOREIGN KEY (`tipos_idtipos`) REFERENCES `tipos` (`idtipos`),
  ADD CONSTRAINT `mensagens_ibfk_3` FOREIGN KEY (`grupos_idgrupos`) REFERENCES `grupos` (`idgrupos`),
  ADD CONSTRAINT `mensagens_ibfk_4` FOREIGN KEY (`autores_idautores`) REFERENCES `autores` (`idautores`);

--
-- Restrições para a tabela `textos_in_idiomas`
--
ALTER TABLE `textos_in_idiomas`
  ADD CONSTRAINT `textos_in_idiomas_ibfk_2` FOREIGN KEY (`idiomas_ididiomas`) REFERENCES `idiomas` (`ididiomas`),
  ADD CONSTRAINT `textos_in_idiomas_ibfk_1` FOREIGN KEY (`textos_idtextos`) REFERENCES `textos` (`idtextos`);

--
-- Restrições para a tabela `tipos_in_idiomas`
--
ALTER TABLE `tipos_in_idiomas`
  ADD CONSTRAINT `tipos_in_idiomas_ibfk_1` FOREIGN KEY (`idiomas_ididiomas`) REFERENCES `idiomas` (`ididiomas`),
  ADD CONSTRAINT `tipos_in_idiomas_ibfk_2` FOREIGN KEY (`tipos_idtipos`) REFERENCES `tipos` (`idtipos`);
