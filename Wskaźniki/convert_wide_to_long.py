import pandas as pd
import argparse

# Mapa kwartałów na miesiące (Q1 → Styczeń, Q2 → Kwiecień, itd.)
QUARTER_TO_MONTH = {
    "Q1": 1,  # Styczeń
    "Q2": 4,  # Kwiecień
    "Q3": 7,  # Lipiec
    "Q4": 10  # Październik
}


def convert_wide_to_long(input_file, output_file, data_type_id, frequency_id, presentation_type_id):
    """
    Konwertuje plik Excel z formatu szerokiego (wide) do formatu długiego (long).

    :param input_file: Ścieżka do pliku wejściowego XLSX.
    :param output_file: Ścieżka do pliku wyjściowego CSV.
    :param data_type_id: ID typu danych (np. 2 dla bezrobocia).
    :param frequency_id: ID częstotliwości (np. 3 dla miesięcznych danych).
    :param presentation_type_id: ID sposobu prezentacji (np. 2 dla procentów).
    """
    # Wczytaj plik Excel
    df = pd.read_excel(input_file)

    # Rozwinięcie formatu szerokiego na długi
    df_melted = df.iloc[:, 4:].melt(var_name="Period", value_name="Value")

    # Konwersja wartości na string (zapobieganie błędom)
    df_melted['Period'] = df_melted['Period'].astype(str).str.strip()

    # Usunięcie pustych wartości
    df_melted = df_melted.dropna(subset=['Period'])

    # Podział "YYYY Mxx" lub "YYYY Qx" na rok i okres (lub sam rok)
    split_period = df_melted['Period'].str.split(' ', expand=True)

    # Obsługa różnych formatów dat:
    if split_period.shape[1] == 2:
        # Format "YYYY Mxx" lub "YYYY Qx"
        df_melted[['Year', 'PeriodType']] = split_period

        # Konwersja miesiąca "M01", "M02" → "01", "02" lub kwartału "Q1", "Q2" → "01", "04" itd.
        df_melted['Month'] = df_melted['PeriodType'].apply(
            lambda x: int(x.replace("M", "")) if "M" in x else QUARTER_TO_MONTH.get(x, None)
        )

    elif split_period.shape[1] == 1:
        # Format tylko "YYYY" (przypisujemy styczeń jako domyślny miesiąc)
        df_melted['Year'] = split_period[0]
        df_melted['Month'] = 1  # Domyślnie ustawiamy styczeń

    else:
        print("🚨 Błąd: Niepoprawne formaty w kolumnie `Period`. Sprawdź dane wejściowe.")
        return

    # Konwersja roku i miesiąca na pełną datę
    df_melted['Date'] = pd.to_datetime(
        df_melted[['Year', 'Month']].astype(str).agg('-'.join, axis=1), format='%Y-%m'
    )

    # Wybór finalnych kolumn
    df_final = df_melted[['Date', 'Value']].copy()
    df_final.insert(0, "DataTypeId", data_type_id)
    df_final.insert(1, "FrequencyId", frequency_id)
    df_final.insert(2, "PresentationTypeId", presentation_type_id)

    # Zapisanie do CSV
    df_final.to_csv(output_file, index=False, sep=";")

    print(f"✅ Plik zapisany: {output_file}")


# 🔹 Przykładowe wywołanie skryptu
convert_wide_to_long(
    input_file="PKB/PKB nominalne kwartalne.xlsx",
    output_file="PKB/PKB nominalne kwartalne.csv",
    data_type_id=2,
    frequency_id=2,
    presentation_type_id=1
)

convert_wide_to_long(
    input_file="PKB/PKB nominalne roczne.xlsx",
    output_file="PKB/PKB nominalne roczne.csv",
    data_type_id=2,
    frequency_id=1,
    presentation_type_id=1
)

convert_wide_to_long(
    input_file="PKB/PKB realne kwartalne.xlsx",
    output_file="PKB/PKB realne kwartalne.csv",
    data_type_id=2,
    frequency_id=2,
    presentation_type_id=5
)

convert_wide_to_long(
    input_file="PKB/PKB realne roczne.xlsx",
    output_file="PKB/PKB realne roczne.csv",
    data_type_id=2,
    frequency_id=1,
    presentation_type_id=6
)
