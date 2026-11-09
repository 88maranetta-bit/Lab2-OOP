<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:template match="/">
		<html>
			<head>
				<meta charset="utf-8"/>
				<title>Електронна таблиця - КНУ та К-27</title>
				<style>
					body {
					font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
					background-color: #34495E;
					margin: 0;
					padding: 30px;
					color: #2C3E50;
					}
					.app-container {
					max-width: 1100px;
					margin: auto;
					background: #ECF0F1;
					padding: 25px;
					border-radius: 10px;
					box-shadow: 0 10px 25px rgba(0,0,0,0.4);
					}
					.header-panel {
					background: #641E16;
					color: white;
					padding: 15px 20px;
					border-radius: 6px;
					display: flex;
					justify-content: space-between;
					align-items: center;
					}
					.header-panel h2 { margin: 0; font-size: 20px; }
					.toolbar {
					margin: 15px 0;
					display: flex;
					gap: 10px;
					}
					.btn {
					background-color: #2980B9;
					color: white;
					border: none;
					padding: 8px 15px;
					border-radius: 4px;
					cursor: pointer;
					font-weight: bold;
					}
					.btn:hover { background-color: #1F618D; }
					.table-container {
					background: white;
					border-radius: 6px;
					overflow: hidden;
					border: 1px solid #BDC3C7;
					}
					table {
					width: 100%;
					border-collapse: collapse;
					}
					th {
					background-color: #5D6D7E;
					color: white;
					padding: 12px;
					text-align: left;
					font-size: 14px;
					}
					td {
					border-bottom: 1px solid #E5E7E9;
					padding: 10px 12px;
					font-size: 13px;
					}
					tr:hover {
					background-color: #EBF5FB;
					cursor: pointer;
					}
					.badge {
					background-color: #800020;
					color: white;
					padding: 3px 8px;
					border-radius: 10px;
					font-size: 11px;
					}
				</style>
			</head>
			<body>
				<div class="app-container">
					<div class="header-panel">
						<h2>Табличний процесор: База даних КНУ та група К-27</h2>
					</div>

					<div class="toolbar">
						<button class="btn" onclick="alert('Дані успішно завантажено з XML!')">Оновити дані</button>
						<button class="btn" style="background-color: #641E16;" onclick="window.print()">Друк / Експорт</button>
					</div>

					<div class="table-container">
						<table>
							<tr>
								<th>№</th>
								<th>П.І.П. особи</th>
								<th>Департамент / Група</th>
								<th>Кафедра</th>
								<th>Посада / Статус</th>
								<th>Освіта / Деталі балів</th>
							</tr>
							<xsl:for-each select="Staff/Employee">
								<tr>
									<td>
										<b>
											<xsl:value-of select="@id"/>
										</b>
									</td>
									<td>
										<xsl:value-of select="FullName"/>
									</td>
									<td>
										<xsl:value-of select="@department"/>
									</td>
									<td>
										<xsl:value-of select="Chair"/>
									</td>
									<td>
										<span class="badge">
											<xsl:value-of select="@type"/>
										</span>
									</td>
									<td>
										<xsl:value-of select="Education/Institution"/>
									</td>
								</tr>
							</xsl:for-each>
						</table>
					</div>
				</div>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>