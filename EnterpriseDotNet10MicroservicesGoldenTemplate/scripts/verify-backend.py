from pathlib import Path
import re,xml.etree.ElementTree as ET
root=Path(__file__).resolve().parents[1]
errors=[]
for p in root.glob('src/**/*.csproj'):
    try: ET.parse(p)
    except Exception as e: errors.append(f'Invalid XML: {p}: {e}')
for p in root.glob('src/**/*.cs'):
    text=p.read_text(encoding='utf-8')
    if '\t' in text: errors.append(f'Tab indentation: {p}')
    if re.search(r'namespace [^;{]+;\s*$',text): pass
# Repository procedure references must exist in database scripts.
procs=set()
for p in root.glob('database/*.sql'):
    procs.update(re.findall(r'CREATE OR ALTER PROCEDURE\s+([\w\.\[\]]+)',p.read_text(encoding='utf-8'),re.I))
for p in root.glob('src/**/*.cs'):
    for proc in re.findall(r'"(dbo\.usp_[A-Za-z0-9_]+)"',p.read_text(encoding='utf-8')):
        if proc not in procs: errors.append(f'Missing SQL procedure contract {proc} referenced by {p}')
if errors:
    print('FAILED'); print('\n'.join(errors)); raise SystemExit(1)
print('PASS: project XML, indentation policy, and repository→stored-procedure references are internally consistent.')
