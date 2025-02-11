import http.server
import socketserver
import _thread
import webbrowser

#? pip install -r requirements.txt --no-index --find-links
#? pyinstaller  --icon=app.ico --onefile server.py

def start_server():
    handler = http.server.SimpleHTTPRequestHandler
    port = 8000
    httpd = socketserver.TCPServer(("localhost", port), handler)
    print(f"Serving on port {port}")
    httpd.serve_forever()

_thread.start_new_thread(start_server,())
url = 'http://localhost:8000'
webbrowser.open_new(url)
while True:
    pass